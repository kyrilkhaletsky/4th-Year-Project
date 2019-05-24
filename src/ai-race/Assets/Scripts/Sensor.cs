using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

    RaycastHit Hit;
    Vector3 forward;
    Vector3 right;
    Vector3 left;
    Vector3 rightForward;
    Vector3 leftForward;

    public int sensorLenght;
    public double[] sensors;
    public int MAX_LEN = 30;

    //Create 5 sensors and set their position relative to the car
    void Start() {
        if (RaceSelect.SelectedRace == 2) {
            sensorLenght = MAX_LEN;
        } else {
            sensorLenght = LearningModeScript.SensorLength;
        }  
        
        sensors = new double[5];   
        left = new Vector3(-0.6f, 0, 0.7f);
        leftForward = new Vector3(-0.3f, 0, 1f);
        forward = Vector3.forward;
        rightForward = new Vector3(0.3f, 0, 1f);
        right = new Vector3(0.6f, 0, 0.7f);
    }

    //Populate sensors[] with distances from car to collider based on each sensor and its length
    void FixedUpdate() {
        sensors[0] = GetSensor(left, sensorLenght);
        sensors[1] = GetSensor(leftForward, sensorLenght + 5);
        sensors[2] = GetSensor(forward, sensorLenght + 20);
        sensors[3] = GetSensor(rightForward, sensorLenght + 5);
        sensors[4] = GetSensor(right, sensorLenght);
    }

    double GetSensor(Vector3 direction, int length) {
        Vector3 sensor = transform.TransformDirection(direction);    
        //If the sensor ray is in contact with something
        if (Physics.Raycast(transform.position, sensor * length, out Hit)) {
            //Set the sensor to red and return the distance between car and collider
            if (Hit.distance < length) {
                Debug.DrawRay(transform.position, sensor * length, Color.red, 0, true);
                return 1f - Hit.distance / length;
            //Otherwise return 0 and set the colour of the sensor ray to green
            } else {
                Debug.DrawRay(transform.position, sensor * length, Color.green, 0, true);
            }
        } else {
            Debug.DrawRay(transform.position, sensor * length, Color.green, 0, true);
        }
        return 0;
    }
}
