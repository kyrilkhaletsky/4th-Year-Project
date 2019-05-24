using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lap : MonoBehaviour {

    public GameObject FinishLineTrigger;
    public GameObject CheckPointTrigger;
    public GameObject FinishRaceTrigger;
    public GameObject BestTime;
    public GameObject LapCount;

    public static float currentTime;
    public static float bestTime;
    public static int lapCount = 0;
    public int TotalLaps;
    public int SelectedRace;

    //If competition, set the lap finished amount
    void Start() {
        SelectedRace = RaceSelect.SelectedRace;
        if (SelectedRace == 2) {
            TotalLaps = RaceSelect.NoOfLaps;
            LapCount.GetComponent<Text>().text = lapCount + "/" + TotalLaps;
        }
    }

    //If its a competition activate the finish game trigger on last lap
    void Update() {
        if (SelectedRace == 2 && lapCount == TotalLaps && CheckPointTrigger.activeSelf == false) {
            FinishRaceTrigger.SetActive(true);
        }
    }

    //Every time the car crosses the finish line
    void OnTriggerEnter(Collider other) {
        if (other.tag == "UserCar") {
            //Set the laptime as the current time
            currentTime = LapTimeManager.lapTime;

            //If its the first lap
            if (lapCount == 1 && SelectedRace != 2) {
                //Set first lap time as best lap and format it
                bestTime = LapTimeManager.lapTime;
                BestTime.GetComponent<Text>().text = ""
                    + LapTimeManager.Convert(LapTimeManager.minutes)
                    + ":" + LapTimeManager.Convert(LapTimeManager.seconds)
                    + "." + LapTimeManager.milliCount.Substring(0, 2);
            }

            lapCount++;

            //If the current time is better than the current best time
            if (currentTime < bestTime && SelectedRace != 2) {
                //Set a new best time and format it
                bestTime = currentTime;
                BestTime.GetComponent<Text>().text = ""
                    + LapTimeManager.Convert(LapTimeManager.minutes)
                    + ":" + LapTimeManager.Convert(LapTimeManager.seconds)
                    + "." + LapTimeManager.milliCount.Substring(0, 2);
            }

            //Set lap count, reset timer and start process again
            if (SelectedRace == 2) {
                LapCount.GetComponent<Text>().text = lapCount + "/" + TotalLaps;
            } else {
                LapCount.GetComponent<Text>().text = "" + lapCount;
            }

            LapTimeManager.ResetTimer();
            CheckPointTrigger.SetActive(true);
            FinishLineTrigger.SetActive(false);
        }
    }
}
