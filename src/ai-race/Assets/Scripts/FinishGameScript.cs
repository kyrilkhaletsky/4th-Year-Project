using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishGameScript : MonoBehaviour {

    public GameObject FinishView;
    public GameObject HUD;
    public GameObject RacePosition;

    //Set position which the user finished as in the race
    void Update() {
        RacePosition.GetComponent<Text>().text = RPS_ScreenUI.RacePosition;
    }

    //Brings up the finish game view and after 1.5 seconds stops the car if its the user car
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "UserCar") {
            Invoke("DelayedFunction", 1.5f);

            FinishView.SetActive(true);
            HUD.SetActive(false);
            PauseMenu.ResetRaceFeatures();
        }
    }

    //Delayed halt to the game and pause the audio to prevent looping engine sound
    private void DelayedFunction() {
        AudioListener.pause = true;
        Time.timeScale = 0;
    }
}
