using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DriveWheel {
    RWD,
    FWD,
    AWD
}

public enum Driver {
    USER,
    AI,
    UnityAI,
    BackProp
}

public class CarPhysics : MonoBehaviour {

    //[0] = FL Wheel
    //[1] = FR Wheel
    //[2] = BL Wheel
    //[3] = BR Wheel
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public Transform[] wheelMeshes = new Transform[4];
    public Transform CenterOfMass;
    public Rigidbody car_rigidbody;
    public GameObject speedText;
    public CarController controller;

    public DriveWheel driveWheel;
    public Driver driver;
    public float maxBrakeTorque;
    public float dropSpeed;
    public float maxSpeed;
    public float maxSteer;
    public float maxTorque;
    public float drag;
    public bool Brake;
    public bool IsBraking;
    public static float CarSpeed;
    public float GENETIC_THRESHOLD = 0.8f;
    public float BACKPROP_THRESHOLD = 0.2f;

    //initialise objects
    void Start() {
        controller = new CarController();
        //add centre of mass
        car_rigidbody = GetComponent<Rigidbody>();
        car_rigidbody.centerOfMass = CenterOfMass.localPosition;      
    }

    //FixedUpdate for physics
    void FixedUpdate() {
        UpdateWheelPositions(wheelColliders, wheelMeshes);

        //Steering depending if input from AI or User (left/right arrow)
        if (driver == Driver.UnityAI) {
            float steer = CarAIControl.steer;
            Steering(steer);
        } else if (driver == Driver.USER) {
            float steer = Input.GetAxis("Horizontal");
            Steering(steer);
        } else if (driver == Driver.AI) {
            //turn a range of 0/1 into a range of -1/1
            float steer = (float)(controller.GeneticSteering);
            steer = (steer - 0.5f) * 2;
            Steering(steer);    
        } else if (driver == Driver.BackProp) {
            //turn a range of 0/1 into a range of -1/1
            float steer = (float)(controller.BackPropSteering);
            steer = (steer - 0.5f) * 2;
            Steering(steer);
        }

        //Torque depending if input from AI or User (up/down arrow)
        if (driver == Driver.UnityAI) {
            float scaledTorque = CarAIControl.accel * maxTorque;
            Torque(scaledTorque);
        } else if (driver == Driver.USER) {
            float scaledTorque = Input.GetAxis("Vertical") * maxTorque;
            Torque(scaledTorque);
        } else if (driver == Driver.AI) {
            float scaledTorque = (float)controller.GeneticEngine * maxTorque;
            Torque(scaledTorque);
        } else if (driver == Driver.BackProp) {
            float scaledTorque = (float)controller.BackPropEngine * maxTorque;
            Torque(scaledTorque);
        }

        //Apply brakes depending on input from AI or User (space)
        if (driver == Driver.USER) {
            if (Input.GetButton("Jump")) {
                Brake = true;
            } else {
                Brake = false;
            }
            ApplyBrakes(Brake);
        } else if (driver == Driver.AI) {
            if ((float)controller.GeneticEngine < GENETIC_THRESHOLD) {
                Brake = true;
            } else {
                Brake = false;
            }
            ApplyBrakes(Brake);
        } else if (driver == Driver.BackProp) {
            if ((float)controller.BackPropEngine < BACKPROP_THRESHOLD) {
                Brake = true;
            } else {
                Brake = false;
            }
            ApplyBrakes(Brake);
        }

        //Change speed value depending on velocity of the car
        if (car_rigidbody.tag == "SelectedCar") {
            speedText = GameObject.Find("Speed");
            speedText.GetComponent<Text>().text = Speed().ToString("f0");
        }

        //Aplly drag to the car
        car_rigidbody.velocity = car_rigidbody.velocity * drag;
    }

    public void Steering(float steer) {  
        float finalAngle = steer * maxSteer;
        
        //Turn left wheel
        wheelColliders[0].steerAngle = finalAngle;
        //Turn right wheel
        wheelColliders[1].steerAngle = finalAngle;
    }

    public void Torque(float scaledTorque) {
        //If speed less than dropSpeed, increase acceleration (refer to docs)
        if (Speed() < dropSpeed) {
            scaledTorque = Mathf.Lerp(scaledTorque, scaledTorque / 3f, Speed() / dropSpeed);
        //If speed greater than dropSpeed, decrease acceleration (refer to docs)
        } else {
            scaledTorque = Mathf.Lerp(scaledTorque / 3f, 0, (Speed() - dropSpeed) / (maxSpeed - dropSpeed));
        }

        //Apply torque to specific wheels depending on drivetrain
        if (driveWheel == DriveWheel.AWD) {
            for (int i = 0; i < 4; i++) {
                wheelColliders[i].motorTorque = scaledTorque;
            }
        } else if (driveWheel == DriveWheel.FWD) {
            wheelColliders[0].motorTorque = scaledTorque;
            wheelColliders[1].motorTorque = scaledTorque;
        } else if (driveWheel == DriveWheel.RWD) {
            wheelColliders[2].motorTorque = scaledTorque;
            wheelColliders[3].motorTorque = scaledTorque;
        }
    }

    public void ApplyBrakes(bool Brake) {
        //if currently braking apply brakes to all wheels
        if (Brake) {
            IsBraking = true;
            for (int i = 0; i < 4; i++) {
                wheelColliders[i].brakeTorque = maxBrakeTorque;
            }
        //Elsewise release the brakes
        } else {
            IsBraking = false;
            for (int i = 0; i < 4; i++) {
                wheelColliders[i].brakeTorque = 0;
            }
        }
    }

    //Makes the wheel model rotate along with its wheel colliders
    void UpdateWheelPositions(WheelCollider[] col, Transform[] tr){
        //For each wheel apply a quaternion to create rotation on the wheel transform
        for (int i = 0; i < 4; i++){
            Vector3 pos = tr[i].position;
            Quaternion rot = tr[i].rotation;
            col[i].GetWorldPose(out pos, out rot);
            //If its the FL or RL wheel rotate the wheels 180 degrees on the Z-axis (refer to docs)
            if (i == 0 || i == 2){
                rot = rot * Quaternion.Euler(new Vector3(0, 0, 180));
            } else{
                rot = rot * Quaternion.Euler(new Vector3(0, 0, 0));
            }
            tr[i].position = pos;
            tr[i].rotation = rot;
        }     
    }

    //Return speed of the car, 3.6f to get km/h or 2.237f to get miles
    public float Speed() {
        CarSpeed = car_rigidbody.velocity.magnitude * 3.6f;
        return CarSpeed;
    }
}