using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;



public class StickyPlatform : MonoBehaviour
{
    private Vector3 lastPlatformPosition; // Stores the platform's position from previous frame, used to calculate how much the platform has moved
    private GameObject player; // Reference to the player object when standing on the platform 

    // Start is called before the first frame update
  private void Start()
    {
        lastPlatformPosition = transform.position; // Initiates the lastPlatformPosition with the starting position 
    }

    private void OnCollisionEnter(Collision collision) // Triggered when another object collides with the platform
    {
        if (collision.gameObject.name == "Player") // Check if the colliding object's name is Player
        {
            player = collision.gameObject; // Stores the player object in the player variable so that the script can manipulate its position later
            lastPlatformPosition = transform.position; // Resets lastPlatformPosition to the platform's current position to avoid applying outdated movement offsets or incorrect movements
        }
    }
    private void OnCollisionExit(Collision collision) // Triggered when the player stops colliding with the platform
    {
        if (collision.gameObject.name == "Player") // Check if the exiting object is the Player
        {
            player = null; // Sets the player variable to null, meaning no player is being tracked
        }
    }

    // Update is called once per frame
    private void Update()
    {
       if (player != null)
        {
            // Calculate how much the platform has moved since the last frame
            Vector3 platformMovement = transform.position - lastPlatformPosition;

            // Apply this movement offset to the player's position
            player.transform.position += platformMovement;

            // Update the last position for the next frame
            lastPlatformPosition = transform.position;
        }
    }
}
