using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.IO;
using System.Globalization;

public class GeneticController : MonoBehaviour {

    public FitnessMeasure fitnessMeasure;
    public GameObject Finishtrigger;
    public GameObject HalfWaytrigger;
    public GameObject CarControl;
    public Text PopulationNumberText;
    public Text GenerationText;
    public Text MutationText;
    public Text PopulationText; 
    public Text CurrentChild;
    public Text EngineText;
    public Text SteeringText;
    public Text FatherText;
    public Text MotherText;
    public Text TimeScale;
    public Text CurrentCar;
    public Slider Slider;
    public Button nextChild;
    Rigidbody Rigidbody;
    Vector3 Position;
    GeneticNetwork[] Cars;
    GeneticWeights saveLoad;
    CarController controller;

    public static int Generation = 0;
    public static int currentChild = 0;
    private string Display = "";
    public List<double> Outputs;
    public double[] Inputs;
    public double[] Fitness;
    public double driveTime = 0;
    public int Population;
    public float Probability;
    public int TWO_LAPS = 7400;

    //Initialise various methods and arrays
    void Start() {
        fitnessMeasure = LearningModeScript.fitnessMeasure;
        Population = LearningModeScript.Population;
        Probability = LearningModeScript.Probability;
        nextChild.onClick.AddListener(NextChild);
        Rigidbody = GetComponent<Rigidbody>();
        controller = new CarController();
        saveLoad = new GeneticWeights();
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.AI;
        Fitness = new double[Population];
        Outputs = new List<double>();
        Position = transform.position;

        //Create cars of size population
        Cars = new GeneticNetwork[Population];

        //Initiate list of weights of size pupulation 
        for (int i = 0; i < Population; i++) {
			Cars[i] = new GeneticNetwork();
        }
    }

    public void Update() {
        //set timescale to the slider value
        Time.timeScale = Slider.value;
        driveTime += Time.deltaTime;

        //if the timescale is > 1 turn off sound (prevents sped up sound (annoying to hear))
        if (Slider.value > 1) {
            AudioListener.volume = 0f;
        } else {
            AudioListener.volume = 1f;
        }

        //if the car is not moving kill it as long as the countdown timer started
        if (driveTime > 3 && CarPhysics.CarSpeed < 0.1 && CarControl.activeSelf == true) {
            OnCollisionEnter(null);
        }     
        if (fitnessMeasure == FitnessMeasure.LapTime && Fitness[currentChild] > TWO_LAPS) {
            OnCollisionEnter(null);
        }
        DisplayHUD();
    }

    //car physics
    void FixedUpdate() {
        //get inputs from sensors
        Inputs = GetComponent<Sensor>().sensors;

        //feed forward the inputs to create outputs based on current weights
        Outputs = Cars[currentChild].FeedForward(Inputs);

        //apply outputs to steering and engine values
        controller.GeneticSteering = (double)Outputs[0];
        controller.GeneticEngine = (double)Outputs[1];

        //increate fitness of current child based on distance travelled
        Fitness[currentChild] += Vector3.Distance(Position, transform.position);
		Position = transform.position;
    }

    //kill current child if the user pressed next child button
    public void NextChild() {
        //SaveChild();
        OnCollisionEnter(null);
        //LoadChild();       
    }

    //save weights
    public void SaveChild() {
        //get weights of current child and add them to a list
        List<double[][]> weights = Cars[currentChild].GetWeights();
        string path = Application.dataPath + "/geneticWeights.text";

        //write the list to a text file
        SaveWeights.WriteToFile(path, weights);
    }
   
    //load weights from text file
    public void LoadChild() {
        string path = Application.dataPath + "/geneticWeights.text";
        Cars[currentChild].SetWeights(saveLoad.LoadWeights(path));
    }

    //Display various Learning Mode display features
    public void DisplayHUD() {
        MutationText.GetComponent<Text>().text = "Mutation Probability: " + Probability * 100 + "%";
        GenerationText.GetComponent<Text>().text = "Generation: " + Generation;
        EngineText.GetComponent<Text>().text = "Engine: " + Outputs[1].ToString("f3");
        SteeringText.GetComponent<Text>().text = "Steering: " + Outputs[0].ToString("f3");
        PopulationNumberText.GetComponent<Text>().text = "Population: " + Population;
        CurrentChild.GetComponent<Text>().text = "Fitness: " + Fitness[currentChild].ToString("f3");
        TimeScale.GetComponent<Text>().text = "Timescale: " + Slider.value;
        CurrentCar.GetComponent<Text>().text = "Current Child: " + (currentChild + 1);
    }

    //When a car collides kill the child
    public void OnCollisionEnter (Collision col) {     
        CalculateFitness();
        ResetCarPosition();

        //display the current child fitness
        Display += "Child " + (currentChild + 1) + ":  " + Fitness[currentChild].ToString("f2") + "\n";
        PopulationText.GetComponent<Text>().text = Display;

        //if all children have died
        if (currentChild == Fitness.Length - 1) {
            int[] FitestParents = new int[2];

            //find 2 of the fittest parents
            FitestParents = FindFittestParents(Fitness);

            GeneticNetwork Father = Cars[FitestParents[0]];
            GeneticNetwork Mother = Cars[FitestParents[1]];

            //create a new generation of children based on 2 fittest parents
            for (int i = 0; i < Population; i++){
                Fitness[i] = 0;
                Cars[i] = new GeneticNetwork(Father, Mother, Probability);
            }
            //increment generation, reset current child and fitness display
            Generation++;
            currentChild = -1;
            Display = "";
        }
        currentChild++;      
	}

    //Calculate fitness of children depending on user selection
    public void CalculateFitness() {
        switch (fitnessMeasure) {
            //divides the distance travelled by the drivetime (promotes faster driving)
            case FitnessMeasure.DriveTime:
                Fitness[currentChild] = (Fitness[currentChild] * Fitness[currentChild]) / driveTime;
                break;
            case FitnessMeasure.LapTime:
                Fitness[currentChild] = (Fitness[currentChild] * Fitness[currentChild]) / driveTime;
                break;
            //default returns the distance travelled (promotes safer driving)
            default:
                break;
        }
    }

    //Retrieve fittest parents
    public int[] FindFittestParents(double[] Fitness) {
        int[] FittestParents = new int[2];
        double MaxFather = 0; //Biggest Value
        double MaxMother = 0; //2nd Biggest Value

        //For each fitness value
        for (int i = 0; i < Fitness.Length; i++) {
            //assign current as temp
            double temp = Fitness[i];
            //and check if temp is larger than highest
            if (temp > MaxFather) {
                MaxMother = MaxFather;
                MaxFather = temp;
                FittestParents[0] = i;
            //and find second largest
            } else if (temp > MaxMother && temp <= MaxFather) {
                MaxMother = temp;
            }
        }
        //assign -1 to father
        Fitness[FittestParents[0]] = -1;

        //find index of mother
        FittestParents[1] = Array.IndexOf(Fitness, MaxMother);

        //display fitness of mother and father on screen
        FatherText.GetComponent<Text>().text = "Father: Child " + (FittestParents[0] + 1) + " (" + MaxFather.ToString("f1") + ")";
        MotherText.GetComponent<Text>().text = "Mother: Child " + (FittestParents[1] + 1) + " (" + MaxMother.ToString("f1") + ")";

        return FittestParents;
    }

    //Reset car posision, drivetime, lap timers, lap sensors
    public void ResetCarPosition() {
        Rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(1023.655f, 103.1957f, 728.5773f);
        transform.rotation = Quaternion.Euler(0.52f, -45.872f, -0.7130001f);
        Position = transform.position;
        driveTime = 0;
        LapTimeManager.ResetTimer();
        Finishtrigger.SetActive(false);
        HalfWaytrigger.SetActive(true);
    }
}