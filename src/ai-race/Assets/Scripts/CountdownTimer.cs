using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    public AudioSource GetReady;
    public AudioSource GoAudio;
    public GameObject Countdown;
    public GameObject LapTimer;
    public GameObject CarControl;

	void Start () {
        StartCoroutine(CountStart());
	}

    IEnumerator CountStart() {
        //Start countdown after 3 seconds
        yield return new WaitForSeconds(3);

        Countdown.GetComponent<Text>().text = "3";
        GetReady.Play();
        Countdown.SetActive(true);

        yield return new WaitForSeconds(1);
        Countdown.SetActive(false);
        Countdown.GetComponent<Text>().text = "2";
        GetReady.Play();
        Countdown.SetActive(true);

        yield return new WaitForSeconds(1);
        Countdown.SetActive(false);
        Countdown.GetComponent<Text>().text = "1";
        GetReady.Play();
        Countdown.SetActive(true);

        //Display "GO" for extra 2 seconds and activate controls and timer
        yield return new WaitForSeconds(1);
        Countdown.SetActive(false);
        Countdown.GetComponent<Text>().text = "GO";
        GoAudio.Play();
        Countdown.SetActive(true);

        //If its competition mode dont show the lap timer (posision instead)
        if (RaceSelect.SelectedRace != 2) {
            LapTimer.SetActive(true);
        }
        
        CarControl.SetActive(true);
        CarAIControl.driving = true;

        yield return new WaitForSeconds(2);
        Countdown.SetActive(false);
    }
}
