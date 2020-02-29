using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour {

    Animator anim;

    public float speed;
    private float waitTime;
    public float startWaitTime;

    //find player
    private Transform playerPos;

    public Transform[] moveSpots;
    private int randomSpot;

    //set "chase" area
    public float lookRadius = 15f;

    private NavMeshAgent agent;

    //audio variables
    public AudioClip sfxdog;
    public AudioClip sfxstun;
    public AudioSource DogSounds;

    public bool Stun;
    public Text stunText;


    private void Start()
    {
        anim = GetComponent<Animator>();

        stunText.text = "";
        Stun = false;

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);

        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Stun == false)
        {
            
            //track distance
            float distance = Vector3.Distance(playerPos.position, transform.position);
            //in "chase" or patrol
            if (distance <= lookRadius)
            {
                agent.SetDestination(playerPos.position);
                Debug.Log("Player Detected.");

                if (distance <= agent.stoppingDistance)
                {
                    anim.SetInteger("Anim", 1);
                    //pounce
                    FacePlayer();
                }
            }

            else
            {
                anim.SetInteger("Anim", 0);
                Cycle();
                //alreadyPlayed = false;
                Debug.Log("Player Escaped.");
            }
        }
    }


    //walk around
    void Cycle()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 1.0f)
        {
            randomSpot = Random.Range(0, moveSpots.Length);
            waitTime = startWaitTime;
            Debug.Log("Destination Reached");
        }
        else
        {
            waitTime -= Time.deltaTime;
            Transform target = moveSpots[randomSpot];
            Vector3 rDirection = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(rDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            Debug.Log("Set New Destination.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameObject.Find("ArmNew").GetComponent<Animate>().attack == true && Stun == false)
            {
                StartCoroutine(Stunned());
            }
            if (GameObject.Find("ArmNew").GetComponent<Animate>().attack == false && Stun == false)
            {
                SceneManager.LoadScene("DefeatMenu");
            }
        }
    }

    //look at me you coward
    void FacePlayer()
    {
        Vector3 direction = (playerPos.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        //without breaking your neck pls
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Visualize "Chase" area
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    IEnumerator Stunned()
    {
        anim.SetInteger("Anim", 2);
        DogSounds.clip = sfxstun; // stun sound
        DogSounds.Play();

        stunText.text = "Enemy Stunned! (3)";
        Stun = true;
        yield return new WaitForSecondsRealtime(1f); //time stunned 3

        stunText.text = "Enemy Stunned! (2)";
        yield return new WaitForSecondsRealtime(1f); //time stunned 2

        stunText.text = "Enemy Stunned! (1)";
        anim.SetInteger("Anim", 3);
        yield return new WaitForSecondsRealtime(1f); //time stunned 1
        Stun = false;
        stunText.text = "";

        anim.SetInteger("Anim", 1);
        DogSounds.clip = sfxdog; // idle sound
        DogSounds.Play();
    }
}
