using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeLightScript : MonoBehaviour {

    public GameObject Lights_on;
    public GameObject Lights_off;
    public Rigidbody Car;

    public bool IsBraking;

    //Activate lights off material renderer on start
	void Start () {
        Lights_off.SetActive(true);
    }
	
	void FixedUpdate () {
        IsBraking = Car.GetComponent<CarPhysics>().IsBraking;

        //Activate/deactivate brake light material renderer depending on IsBraking boolean value
        if (IsBraking) {
            Lights_on.SetActive(true);
            Lights_off.SetActive(false);
        } else {
            Lights_off.SetActive(true);
            Lights_on.SetActive(false);
        }
	}
}
