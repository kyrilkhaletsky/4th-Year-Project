using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelectController : MonoBehaviour {

    // 1 = EVO
    // 2 = GTO
    // 3 = Z66
    // 4 = GTX
    public static GameObject SelectedCar;
    public static GameObject SelectedCarUnity;
    public static GameObject SelectedCarGenetic;
    public static GameObject SelectedCarBackProp;
    public static Text SpeedText;
    public List<GameObject> Cars;
    public GameObject EVO;
    public GameObject GTO;
    public GameObject Z66;
    public GameObject GTX;

    public int CarImport;

    private void Start() {

        //Set CarImport as the car selected, and add all cars to a list
        Cars = new List<GameObject> { EVO, GTO, Z66, GTX };
        CarImport = CarSelect.SelectedCar;

        //Activate a specific car in game and assign it to UserCar tag, Driver.User enum and remove it from cars list
        if (CarImport == 2) {
            GTO.SetActive(true);
            SelectedCar = GTO;
            GameObject.Find("GTO_Collider").tag = "UserCar";
            GameObject.Find("GTO").tag = "SelectedCar";
            GameObject.Find("GTO").GetComponent<CarPhysics>().driver = Driver.USER;
            Cars.Remove(GTO);
        } else if (CarImport == 3) {
            Z66.SetActive(true);
            SelectedCar = Z66;
            GameObject.Find("Z66_Collider").tag = "UserCar";
            GameObject.Find("Z66").tag = "SelectedCar";
            GameObject.Find("Z66").GetComponent<CarPhysics>().driver = Driver.USER;
            Cars.Remove(Z66);
        } else if (CarImport == 4) {
            GTX.SetActive(true);
            SelectedCar = GTX;
            GameObject.Find("GTX_Collider").tag = "UserCar";
            GameObject.Find("GTX").tag = "SelectedCar";
            GameObject.Find("GTX").GetComponent<CarPhysics>().driver = Driver.USER;
            Cars.Remove(GTX);
        } else {
            EVO.SetActive(true);
            SelectedCar = EVO;
            GameObject.Find("Evo_Collider").tag = "UserCar";
            GameObject.Find("EVO").tag = "SelectedCar";
            GameObject.Find("EVO").GetComponent<CarPhysics>().driver = Driver.USER;
            Cars.Remove(EVO);
        }

        //Shuffle cars to make a random car spawn with a random algorithm
        Cars = Shuffle(Cars);

        //Set cars to their respective algorithm
        SelectedCarUnity = Cars[0];
        SelectedCarGenetic = Cars[1];
        SelectedCarBackProp = Cars[2];
        SelectedCar.GetComponentInChildren<RPS_ScreenUI>().enabled = true;
    }

    //shuffles list of cars in a random order, based on the Fisher-Yates shuffle
    public static List<GameObject> Shuffle<GameObject>(List<GameObject> Cars) {
        int n = Cars.Count;
        System.Random rnd = new System.Random();
        while (n > 1) {
            int k = (rnd.Next(0, n) % n);
            n--;
            GameObject value = Cars[k];
            Cars[k] = Cars[n];
            Cars[n] = value;
        }
        return Cars;
    }
}
