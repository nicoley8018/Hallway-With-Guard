using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickup : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject playerHand;
    public GameObject Waffle;
    public GameObject Chicken;

    public Text interactText;

    public bool hasChicken;
    public bool hasWaffle;



    void Start()
    {
        interactText.text = "";
        hasChicken = false;
        hasWaffle = false;
    }
    void Update()
    {
        RaycastHit hitInfo;
        //Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, .2f);
        Ray r = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward, Color.red);


        //if we hit something
        if (Physics.Raycast(r, out hitInfo, .5f))
        {
            if (hitInfo.transform.CompareTag("Chicken") && hasChicken == false)
            {
                interactText.text = "Press E(B) to pickup";

                if (Input.GetButtonDown("Submit"))
                {
                    Debug.Log("GotChicken");
                    hasChicken = true;
                    StartCoroutine(PickUpChicken());
                    interactText.text = "";
                }
            }
            if (hitInfo.transform.CompareTag("Waffle"))
            {
                interactText.text = "Press E(B) to pickup";

                if (Input.GetButtonDown("Submit"))
                {
                    Debug.Log("GotWaffle");
                    hasWaffle = true;
                    Waffle.gameObject.SetActive(false);
                    interactText.text = "";
                }
            }
        }
        else
        {
            interactText.text = "";
        }
        
    }

    IEnumerator PickUpChicken()
    {
        yield return new WaitForSecondsRealtime(0.75f); //time until the chicken teleports to hand
        Chicken.gameObject.transform.parent = playerHand.transform;
        Chicken.gameObject.transform.localPosition = new Vector3(-.0072f, -.0029f, -.0147f);
        Chicken.gameObject.transform.localRotation = Quaternion.Euler(-90.754f, -.0009765f, 22.808f);
    }
}
