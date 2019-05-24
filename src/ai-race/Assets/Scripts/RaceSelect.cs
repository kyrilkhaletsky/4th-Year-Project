using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceSelect : MonoBehaviour {

    // 1 = TimeTrial
    // 2 = Competition
    public static int SelectedRace;

    public GameObject LapView;
    public GameObject CarSelect;
    public Text Laps;
    public Slider Slider;
    public static int NoOfLaps;

    //if competition is selected show no of laps slider
    public void Update() {
        if (CarSelect.activeSelf == true) {
            NoOfLaps = (int)Slider.value;
            Laps.GetComponent<Text>().text = "No. of Laps: " + (NoOfLaps);
        }
    }

    //Set selected mode as time trial and turn on car selector
    public void TimeTrial() {
        SelectedRace = 1;
        CarSelect.SetActive(true);
        LapView.SetActive(false);
    }

    //Set selected mode as competition and turn on car selector
    public void Competition() {
        SelectedRace = 2;
        CarSelect.SetActive(true);
        LapView.SetActive(true);
    }
}
