using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScript : MonoBehaviour {

    public GameObject ControlsPanel;
    public GameObject RacingModePanel;
    public GameObject LearningModePanel;
    public GameObject CreditPanel;

    //Show Controls screen
    public void Controls() {
        SetInactive();
        ControlsPanel.SetActive(true);
    }

    //Show Racing Mode screen
    public void RacingMode() {
        SetInactive();
        RacingModePanel.SetActive(true);
    }

    //Show Learning Mode screen
    public void LearningMode() {
        SetInactive();
        LearningModePanel.SetActive(true);
    }

    //Show Credits Screen
    public void Credits() {
        SetInactive();
        CreditPanel.SetActive(true);
    }

    //Turn off all screens
    public void SetInactive() {
        ControlsPanel.SetActive(false);
        RacingModePanel.SetActive(false);
        LearningModePanel.SetActive(false);
        CreditPanel.SetActive(false);
    }
}
