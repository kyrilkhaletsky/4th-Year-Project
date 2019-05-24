using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticDriver : MonoBehaviour {

    public GameObject[] Points = new GameObject[10];
    public GameObject CarControl;
    Rigidbody Rigidbody;
    GeneticNetwork Car;
    GeneticWeights saveLoad;
    CarController controller;
    ResetPosition position;
    Vector3 Position;

    public double driveTime = 0;
    public float WaitTime = 3.0f;
    public static double Steering;
    public static double Engine;
    public List<double> Outputs;
    public double[] Inputs;

    //Initialise objects
    void Start() {
        Rigidbody = GetComponent<Rigidbody>();
        Outputs = new List<double>();
        Car = new GeneticNetwork();
        saveLoad = new GeneticWeights();
        controller = new CarController();
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.AI;
        position = new ResetPosition();
        LoadChild();
    }

    void Update() {
        driveTime += Time.deltaTime;
        //check if a car has stopped for more than 3 seconds (crashed car) and reset position
        if (driveTime > WaitTime) {
            if (Rigidbody.velocity.magnitude < 0.1 && CarControl.activeSelf == true) {
                ResetCurrent();
            }
            driveTime = driveTime - WaitTime;
        }
    }

    //get inputs from sensors, get outputs from Genetic NN
    void FixedUpdate() {
        Inputs = GetComponent<Sensor>().sensors;
        Outputs = Car.FeedForward(Inputs);

        //set outputs to CarController class
        controller.GeneticSteering = (double)Outputs[0];
        controller.GeneticEngine = (double)Outputs[1];
    }

    //Load weights from text file
    public void LoadChild() {
        string path = Application.dataPath + "/geneticWeights.text";
        Car.SetWeights(saveLoad.LoadWeights(path));
    }

    //Resets car position if the car is stuck
    public void ResetCurrent() {
        GameObject closest = position.ResetCurrentCar(Rigidbody, transform.position, Points);
        Rigidbody.velocity = Vector3.zero;
        transform.position = closest.transform.position;
        transform.rotation = closest.transform.rotation;
        Position = transform.position;
    }
}
