using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour {

    public GameObject Loading;
    public Slider Slider;
    public Text Text;
    public int RacingMode;
    public int LearningMode;

    //Launch LearningMode
    public void LoadLearningScene() {
        LearningMode = LearningModeScript.LearningMode;

        if (LearningMode == 2) {
            StartCoroutine(LoadingScreen(1));
        } else {
            StartCoroutine(LoadingScreen(7));
        }
    }

    //Launch coroutine selected game mode
    public void LoadGameScene() {
        RacingMode = RaceSelect.SelectedRace;

        if (RacingMode == 2) {
            StartCoroutine(LoadingScreen(6));
        } else {
            StartCoroutine(LoadingScreen(3));
        }
    }

    //Activate loading screen
    IEnumerator LoadingScreen (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        Loading.SetActive(true);

        //Format percentage of scene which is loaded and show it above slider
        while (operation.isDone == false) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Slider.value = progress;
            Text.text = (int) System.Math.Round(progress * 100f,0) + "%";
            yield return null;
        }
    }
}
