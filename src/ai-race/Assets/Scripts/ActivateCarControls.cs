using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCarControls : MonoBehaviour {

    public GameObject CarControl;
    public GameObject UnityAIControl;
    public GameObject AIControl;
    public GameObject BackPropControl;

    //Set car controls to the selected car model and enable car physics
    void Start () {
        CarControl = CarSelectController.SelectedCar;
        UnityAIControl = CarSelectController.SelectedCarUnity;
        AIControl = CarSelectController.SelectedCarGenetic;
        BackPropControl = CarSelectController.SelectedCarBackProp;

        CarControl.GetComponent<CarPhysics>().enabled = true;
        AIControl.GetComponent<GeneticDriver>().enabled = true;
        AIControl.GetComponent<CarPhysics>().enabled = true;
        BackPropControl.GetComponent<BackPropDriver>().enabled = true;
        BackPropControl.GetComponent<CarPhysics>().enabled = true;
        UnityAIControl.GetComponent<CarAIControl>().enabled = true;
        UnityAIControl.GetComponent<CarPhysics>().enabled = true;
    }
}
