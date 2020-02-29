using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    PlayerControls controls;

    public CharacterController player;
    public Transform groundcheck;

    public float speed = 12.0f;
    public float gravity = -9.8f;
    public float gDistance = 0.4f;
    public float jumpHeight = 3f;

    Vector3 velocity;

    public LayerMask gMask;

    bool isGrounded;


    public float mouseSensitivity = 100.0f;

    public Transform target;

    float xRotation = 0.0f;

    Vector2 rotate;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.RotateCamera.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.RotateCamera.canceled += ctx => rotate = Vector2.zero;
    }

    void RotateCamera()
    {
        Debug.Log("RotateCamera");
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        target.Rotate(Vector3.up* mouseX);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Update()
    {
        Vector2 r = new Vector2(-rotate.y, -rotate.x) * 100f * Time.deltaTime;
        transform.Rotate(r, Space.World);

        isGrounded = Physics.CheckSphere(groundcheck.position, gDistance, gMask);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        player.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        player.Move(velocity * Time.deltaTime);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Dog")
        {
            SceneManager.LoadScene("DefeatMenu");
        }
    }

   
}
