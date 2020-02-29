using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator anim;
    public bool attack;
    public bool cooldown;
    public AudioClip Hitsfx;
    public AudioSource Playersfx;
    public GameObject hitbox;

    // Start is called before the first frame update
    void Start()
    {
        attack = false;
        anim = GetComponent<Animator>();
        anim.SetInteger("Grab", 0);

    }

    // Update is called once per frame
    void Update()
    {
        animate();
    }
    void animate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetInteger("Grab", 1);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetInteger("Grab", 0);
        }
        if (GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == true)
        {
            anim.SetBool("hasChicken", true);
        }
        if (Input.GetButtonDown("Fire1") && GameObject.Find("FPP").GetComponent<Pickup>().hasChicken == true)
        {
            StartCoroutine(Attacking());
        }
    }
    IEnumerator Attacking()
    {
        if (attack == false && cooldown == false)
        {
            Playersfx.Play();

            hitbox.SetActive(true);
            Debug.Log("Attack-using");
            anim.SetInteger("Click", 1);
            attack = true;
            cooldown = true;
            yield return new WaitForSeconds(0.5f); // attack active

            Debug.Log("Attack-cooldown");
            anim.SetInteger("Click", 0);
            attack = false;
            yield return new WaitForSeconds(0.5f); // attack cooldown
            hitbox.SetActive(false);

            cooldown = false;
            Debug.Log("Attack-available");
        }
    }
}
    
