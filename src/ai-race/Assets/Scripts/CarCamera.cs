using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour {

    public Transform car;
    GameObject selectedCar;

    public float distance;
    public float height;
    public float steerRotation;
    public float heightRotation;
    public float zoomRatio;
    public float fieldOfView;
    private float rotation;
    public int REVERSE_ROTATION = 160;

    private void LateUpdate() {
        //Set the camera to follow the selected car
        selectedCar = CarSelectController.SelectedCar;
        if (selectedCar != null) {
            car = selectedCar.transform;
        } else {
            Debug.Log("Camera object not found");
        }

        //Set variables (depending on car)
        float wantedAngle = rotation;
        float wantedHeight = car.position.y + height;
        float carAngle = transform.eulerAngles.y;
        float carHeight = transform.position.y;

        //Find angle and height of camera depending on set variables
        carAngle = Mathf.LerpAngle(carAngle, wantedAngle, steerRotation * Time.deltaTime);
        carHeight = Mathf.LerpAngle(carHeight, wantedHeight, heightRotation * Time.deltaTime);

        //Set new camera angle based on car position and follow the car
        Quaternion currentRotation = Quaternion.Euler(0, carAngle, 0);
        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        Vector3 temp = transform.position;
        temp.y = carHeight;
        transform.position = temp;
        transform.LookAt(car);
    }

    void Update() {
        //Get velocity of the car, if its less than 10km/h (in reverse) turn the camera around (for reversing)
        Vector3 velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if (velocity.z < -3f) {
            rotation = car.eulerAngles.y + REVERSE_ROTATION;
        } else {
            rotation = car.eulerAngles.y;
        }
    }
}
