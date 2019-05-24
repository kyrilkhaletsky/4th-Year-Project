using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelect : MonoBehaviour {

    // 1 = EVO
    // 2 = GTO
    // 3 = Z66
    // 4 = GTX
    public static int SelectedCar;

    public GameObject ShowEVO;
    public GameObject ShowZ66;
    public GameObject ShowGTO;
    public GameObject ShowGTX;
    public GameObject Showcase;
    public GameObject StartGame;

    //Set selected car as EVO and activate car showcase + startgame button
    public void EVO () {
        SelectedCar = 1;
        SetInactive();
        ShowEVO.SetActive(true);
        StartGame.SetActive(true);
    }

    //Set selected car as GTO and activate car showcase + startgame button
    public void GTO() {
        SelectedCar = 2;
        SetInactive();
        ShowGTO.SetActive(true);
        StartGame.SetActive(true);
    }

    //Set selected car as EVO and activate car showcase + startgame button
    public void Z66() {
        SelectedCar = 3;
        SetInactive();
        ShowZ66.SetActive(true);
        StartGame.SetActive(true);
    }

    //Set selected car as GTX and activate car showcase + startgame button
    public void GTX() {
        SelectedCar = 4;
        SetInactive();
        ShowGTX.SetActive(true);
        StartGame.SetActive(true);
    }

    //Turn off all cars from showcase and activate showcase
    public void SetInactive() {
        ShowEVO.SetActive(false);
        ShowZ66.SetActive(false);
        ShowGTO.SetActive(false);
        ShowGTX.SetActive(false);
        Showcase.SetActive(true);
    }
}
