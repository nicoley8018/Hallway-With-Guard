using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorOpen : MonoBehaviour
{
    public Text interactText;
    public GameObject door;
    

    void Start()
    {
        interactText.text = "";
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            interactText.text = "Press E(B) to open";
            if(Input.GetButtonDown("Submit"))
            {
                transform.position = new Vector3(0, -.481f, 0);
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactText.text = "";
    }
}
