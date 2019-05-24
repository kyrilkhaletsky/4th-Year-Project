using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPropController : MonoBehaviour {

    public GameObject[] Points = new GameObject[10];
    public GameObject AutoPanel;
    public GameObject AutoText;
    public Text EngineText;
    public Text SteeringText;
    public Button Reset;
    public Button Restart;
    ResetPosition position;
    BackPropNetwork backProp;
    BackPropWeights saveLoad;
    CarController controller;
    Rigidbody Rigidbody;
    Vector3 Position;

    public double[] Outputs;
    public double[] Inputs;
    public int lapCount;
    public int NoLaps;
    public double LearningRate;
    public bool runOnce = true;

    //Initialise objects
    void Start () {
        Rigidbody = GetComponent<Rigidbody>();
        Reset.onClick.AddListener(ResetCurrent);
        Restart.onClick.AddListener(RestartCurrent);
        LearningRate = LearningModeScript.LearningRate;
        NoLaps = LearningModeScript.NoLaps;
        backProp = new BackPropNetwork();
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.USER;
        controller = new CarController();
        saveLoad = new BackPropWeights();
        Rigidbody.GetComponent<Sensor>().sensorLenght += 5;
        position = new ResetPosition();
    }

    //update engine and steering text on screen
    public void Update() {
        EngineText.GetComponent<Text>().text = "Engine: " + Outputs[1].ToString("f3");
        SteeringText.GetComponent<Text>().text = "Steering: " + Outputs[0].ToString("f3");
    }

    void FixedUpdate() {
        //set training lap count and get inputs from sensors
        lapCount = Lap.lapCount;
        Inputs = GetComponent<Sensor>().sensors;

        //get steering and torque during training
        double steer = Input.GetAxis("Horizontal");
        double torque = Input.GetAxis("Vertical");

        //change steering values to be between -1 and 1
        if (steer < 0) {
            steer = (steer * 0.5) + 0.5;
        } else if (steer > 0) {
            steer = (steer + 1) / 2;
        } else {
            steer = 0.5;
        }

        //if still training
        if (lapCount <= NoLaps) {
            //feed inputs, backpropogate expected values and hide training panel
            backProp.FeedForward(Inputs);
            backProp.Propogate(new double[] { steer, torque }, LearningRate);
            AutoPanel.SetActive(false);

        //when finished training
        } else {
            //set the backprop alg as driver, feed inputs to create outputs
            Rigidbody.GetComponent<CarPhysics>().driver = Driver.BackProp;
            Outputs = backProp.FeedForward(Inputs);

            //set outputs to CarController class
            controller.BackPropSteering = Outputs[0];
            controller.BackPropEngine = Outputs[1];

            //show training panel and tell user to release all controls
            AutoPanel.SetActive(true);         
            if (runOnce) {
                StartCoroutine(CountStart());
                runOnce = false;
            }        
        }
    }

    //Resets car position if the car is stuck
    public void ResetCurrent() {
        GameObject closest = position.ResetCurrentCar(Rigidbody, transform.position, Points);

        Rigidbody.velocity = Vector3.zero;
        transform.position = closest.transform.position;
        transform.rotation = closest.transform.rotation;
        Position = transform.position;
        
        //SaveChild();
    }

    //Restarts training process
    public void RestartCurrent() {
        ResetCarPosition();
        backProp = new BackPropNetwork();
        Lap.lapCount = 0;
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.USER;
        runOnce = true;
    }

    //save weights
    public void SaveChild() {
        //get weights of current child and add them to a list
        List<double[][]> weights = backProp.GetWeights();
        string path = Application.dataPath + "/backPropWeights.text";

        //write the list to a text file
        SaveWeights.WriteToFile(path, weights);
    }

    //load weights from text file
    public void LoadChild() {
        string path = Application.dataPath + "/backPropWeights.text";
        backProp.SetWeights(saveLoad.LoadWeights(path));
    }

    //display text on screen for 2 seconds and then hide it again
    IEnumerator CountStart() {
        AutoText.SetActive(true);
        yield return new WaitForSeconds(3);
        AutoText.SetActive(false);
    }

    //Reset car position and lap timer
    public void ResetCarPosition() {
        Rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(1023.655f, 103.1957f, 728.5773f);
        transform.rotation = Quaternion.Euler(0.52f, -45.872f, -0.7130001f);
        Position = transform.position;

        LapTimeManager.ResetTimer();
    }
}
