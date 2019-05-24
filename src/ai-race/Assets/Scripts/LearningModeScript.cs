using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum FitnessMeasure {
    Distance,
    DriveTime,
    LapTime
}

public class LearningModeScript : MonoBehaviour {

    public static FitnessMeasure fitnessMeasure;
    public static int Population = 8;
    public static int SensorLength = 10;
    public static float Probability = 0.02f;
    public static double LearningRate = 0.033;
    public static int NoLaps = 1;
    public static int LearningMode;

    public GameObject CarSelect;
    public GameObject GeneticAtt;
    public GameObject BackPropAtt;
    public Dropdown dropDown_learningRate;
    public Dropdown dropDown_mutation;
    public Dropdown dropDown;
    public Slider Slider;
    public Slider LapNo;
    public Text PopulationText;
    public Slider SensorSlider;
    public Text SensorText;
    public Text LapText;

    //if genetic alg selected show corresponding panel
    public void Genetic() {
        LearningMode = 2;
        CarSelect.SetActive(true);
        GeneticAtt.SetActive(true);
        BackPropAtt.SetActive(false);
    }

    //if backprop alg selected show corresponding panel
    public void Backpropogation() {
        LearningMode = 1;
        CarSelect.SetActive(true);
        BackPropAtt.SetActive(true);
        GeneticAtt.SetActive(false);
    }

    //Add list of Fitness types and mutations into dropdown menues
    void Start () {
        //Set dropdown as available fitness values
        string[] fitnessNames = Enum.GetNames(typeof(FitnessMeasure));
        List<string> names = new List<string>(fitnessNames);
        dropDown.AddOptions(names);

        //Set dropdown names are mutation percentages
        string[] mutationProbabilities = { "2%", "5%", "10%", "20%" };
        List<string> mutations = new List<string>(mutationProbabilities);
        dropDown_mutation.AddOptions(mutations);

        //Set dropdown names are Learning rate percentages
        string[] learningRate = { "0.3%", "3%", "5%", "10%" };
        List<string> rates = new List<string>(learningRate);
        dropDown_learningRate.AddOptions(rates);
    }

    //Update 2 Dropdown and Population slider values
    void Update() {
        //if genetic is selected
        if (GeneticAtt.activeSelf == true) {
            dropDown_mutation.onValueChanged.AddListener(delegate {
                MutationDropdownValueChanged(dropDown_mutation);
            });
            dropDown.onValueChanged.AddListener(delegate {
                DropdownValueChanged(dropDown);
            });

            //Set population text to the slider value
            Population = (int)Slider.value;
            PopulationText.GetComponent<Text>().text = "Population: " + (Population);

            //Set sensor text to the slider value
            SensorLength = (int)SensorSlider.value;
            SensorText.GetComponent<Text>().text = "Sensor Length: " + (SensorLength) + "m";

        //if backprop is selected
        } else if (BackPropAtt.activeSelf == true) {
            dropDown_learningRate.onValueChanged.AddListener(delegate {
                RateDropdownValueChanged(dropDown_learningRate);
            });

            //show laps text and slider and set sensor length to 30
            NoLaps = (int)LapNo.value;
            LapText.GetComponent<Text>().text = "No. of Laps: " + (NoLaps);
            SensorLength = 30;
        }
    }

    //When dropdown value is changed, also change the fitnessMeasure static var
    void DropdownValueChanged(Dropdown change) {
        fitnessMeasure = (FitnessMeasure)change.value;
    }

    //When dropdown value is changed, also change the Mutation Prob static var
    void MutationDropdownValueChanged(Dropdown change) {
        if(change.value == 0) {
            Probability = 0.02f;
        } else if (change.value == 1) {
            Probability = 0.05f;
        } else if (change.value == 2) {
            Probability = 0.1f;
        } else {
            Probability = 0.2f;
        }
    }

    //When dropdown value is changed, also change the Learning Rate static var
    void RateDropdownValueChanged(Dropdown change) {
        if (change.value == 0) {
            LearningRate = 0.0033;
        } else if (change.value == 1) {
            LearningRate = 0.033;
        } else if (change.value == 2) {
            LearningRate = 0.05;
        } else {
            LearningRate = 0.1;
        }
    }
}
