using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject PauseMenuCanvas;
    public static bool GamePaused = false;

	void Update () {
        //if "Esc" is pressed open/close the pause menu depening on if its open/closed 
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
	}

    //Reset lap times and lap counts
    public static void ResetRaceFeatures() {
        AudioListener.pause = false;
        LapTimeManager.ResetTimer();
        Lap.lapCount = 0;
        Lap.bestTime = 0;
        Lap.currentTime = 0;
        GeneticController.Generation = 0;
    }

    //Activate Pause canvas and freeze the game
    void Pause () {
        PauseMenuCanvas.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;
        GamePaused = true;
    }

    //Deactivate Pause canvas and unfreeze the game
    public void Resume () {
        PauseMenuCanvas.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1;
        GamePaused = false;
    }

    //Unfreeze the game and return to main menu
    public void ReturnToMainMenu() {
        RaceSelect.SelectedRace = 0;
        ResetRaceFeatures();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    //launch help
    public void Help() {
        ResetRaceFeatures();
        SceneManager.LoadScene(4);
    }

    //quit the game
    public void QuitGame() {
        ResetRaceFeatures();
        Debug.Log("QuitGame!");
        Application.Quit();
    }
}
