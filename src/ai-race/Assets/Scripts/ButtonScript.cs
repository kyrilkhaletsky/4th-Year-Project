using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    //return to main menu
    public void ReturnToMainMenu() {
        RaceSelect.SelectedRace = 0;
        SceneManager.LoadScene(0);
    }

    //launch racing mode selection
    public void RacingMode() {
        SceneManager.LoadScene(2);
    }

    //launch learning mode selection
    public void LearningModeSelection() {
        SceneManager.LoadScene(5);
    }

    //launch help
    public void Help() {
        SceneManager.LoadScene(4);
    }

    //quit the game
    public void QuitGame() {
        Debug.Log("QuitGame!");
        Application.Quit();
    }
}
