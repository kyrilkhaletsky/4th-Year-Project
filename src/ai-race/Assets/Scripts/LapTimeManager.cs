using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour {

    public GameObject LapTime;

    public static int minutes;
    public static int seconds;
    public static float milliseconds;
    public static float lapTime;
    public static string milliCount;

    void FixedUpdate () {
        //Turn seconds into milliseconds and remove decimal
        milliseconds += Time.deltaTime * 10;
        milliCount = milliseconds.ToString("f1").Replace(".","");

        //For every 10 milliseonds add 1 second
        if(milliseconds >= 10) {
            milliseconds = 0;
            seconds += 1;
            lapTime += 1;
        }
        //For every 60 seconds add 1 minute
        if (seconds >= 60) {
            seconds = 0;
            minutes += 1;
        }
        //Format the laptime as seen in game
        LapTime.GetComponent<Text>().text = "" 
            + Convert(minutes) + ":" 
            + Convert(seconds) + "." 
            + milliCount.Substring(0, 2);
    }

    //Adds a 0 to singular numbers (1.1:1 to 01.01:01)
    public static string Convert(int x) {
        if (x <= 9) {
            return "0" + x;
        } else {
            return "" + x;
        }
    }

    //Resets timer once game is finished
    public static void ResetTimer() {
        minutes = 0;
        seconds = 0;
        milliseconds = 0;
        lapTime = 0;
    }
}
