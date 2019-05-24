using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    public GameObject Finishtrigger;
    public GameObject HalfWaytrigger;

    // Sets finish line active once halfway line has been triggered
    void OnTriggerEnter(Collider other) {
        if (other.tag == "UserCar") {
            Finishtrigger.SetActive(true);
            HalfWaytrigger.SetActive(false);
        }   
    }
}
