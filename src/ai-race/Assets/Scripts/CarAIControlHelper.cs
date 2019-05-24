using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIControlHelper : MonoBehaviour {

    public GameObject[] MK1 = new GameObject[22];
    public GameObject MKTrigger;
    public int MKTracker;

    //If the tracker is the current node set tracker to the next node
    void Update () {
        for (int i = 0; i < MK1.Length; i++) {
            if (MKTracker == i) {
                MKTrigger.transform.position = MK1[i].transform.position;
            }
        }
	}

    //When a collision happens between car driven by Unity and node
    IEnumerator OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "UnityAI") {
            //set the next node as the tracker
            this.GetComponent<BoxCollider>().enabled = false;
            MKTracker += 1;
            //if its the last node reset it and set 1st node as new node
            if (MKTracker == MK1.Length) {
                MKTracker = 0;
            }
            yield return new WaitForSeconds(1);
            this.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
