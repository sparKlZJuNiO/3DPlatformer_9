using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
   [SerializeField] float movementSpeed = 6f;
   [SerializeField] float jumpForce = 5f;
    [SerializeField] float mouseSensitivity = 1000;
    float xRotation = 0f;
    [SerializeField] Transform playerCamera; // Assign your camera in the inspector

   [SerializeField] Transform groundCheck;
   [SerializeField] LayerMask ground;

    [SerializeField] AudioSource jumpSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor in place
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move in the direction the player is facing
        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);


        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical

        xRotation -= mouseY; // Minus the current mouse Y-axis movement to the current vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical position, prevents it from flipping and limits the camera to 90 degrees in either direction

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotates camera up and down
        transform.Rotate(Vector3.up * mouseX); // Vertically rotates player left/right, turns player's body.

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }

    }


    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 1f, ground);
    }
}
