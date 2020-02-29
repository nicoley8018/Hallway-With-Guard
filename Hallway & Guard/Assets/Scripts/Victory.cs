using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 0) * Time.deltaTime);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Waffle")
        {
            SceneManager.LoadScene("VictoryMenu");
        }
    }
}
