using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPropDriver : MonoBehaviour {

    public GameObject[] Points = new GameObject[10];
    public GameObject CarControl;
    Rigidbody Rigidbody;
    Vector3 Position;
    CarController controller;
    BackPropNetwork backProp;
    BackPropWeights saveLoad;
    ResetPosition position;

    public double driveTime = 0;
    public float WaitTime = 3.0f;
    public static double Steering;
    public static double Engine;
    public double[] Outputs;
    public double[] Inputs;
    
    //Initialise objects
    void Start() {
        Rigidbody = GetComponent<Rigidbody>();
        backProp = new BackPropNetwork();
        Rigidbody.GetComponent<CarPhysics>().driver = Driver.BackProp;
        controller = new CarController();
        saveLoad = new BackPropWeights();
        Rigidbody.GetComponent<Sensor>().sensorLenght += 5;
        position = new ResetPosition();
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

    //load weights from text file
    public void LoadChild() {
        string path = Application.dataPath + "/backPropWeights.text";
        backProp.SetWeights(saveLoad.LoadWeights(path));
    }

    void FixedUpdate() {
        //Load trained car, get inputs from sensors
        LoadChild();
        Inputs = GetComponent<Sensor>().sensors;

        //feed inputs of sensors, get outputs, and set outputs to CarController class
        Outputs = backProp.FeedForward(Inputs);
        controller.BackPropSteering = Outputs[0];
        controller.BackPropEngine = Outputs[1];
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
