using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {

    public GameObject ResetCurrentCar(Rigidbody Body, Vector3 Position, GameObject[] Points) {      
        //set closes distance to infinity
        float closestDistance = Mathf.Infinity;
        GameObject closest = null;

        //check each point and find one thats closes to the car
        foreach (GameObject point in Points) {
            float currentDistance = (point.transform.position - Position).sqrMagnitude;
            if (currentDistance < closestDistance) {
                closestDistance = currentDistance;
                closest = point;
            }
        }
        return closest;
    }
}
