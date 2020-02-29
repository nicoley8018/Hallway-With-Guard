using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Objective : MonoBehaviour
{
    public Text interactText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //has nothing
        if (GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == false && GameObject.Find("FPP").GetComponent<Pickup>().hasWaffle == false)
        {
            interactText.text = "Get Chicken               Get Waffle";
        }

        //has chicken, no waffle
        if (GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == true && GameObject.Find("FPP").GetComponent<Pickup>().hasWaffle == false)
        {
            interactText.text = "Get Waffle";
        }

        //has waffle, no chicken
        if (GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == false && GameObject.Find("FPP").GetComponent<Pickup>().hasWaffle == true)
        {
            interactText.text = "Get Chicken";
        }

        //has everything
        if (GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == true && GameObject.Find("FPP").GetComponent<Pickup>().hasWaffle == true)
        {
            SceneManager.LoadScene("VictoryMenu");
        }
    }
}
