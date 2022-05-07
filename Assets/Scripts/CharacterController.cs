using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    float speedBase = 10;     // Default speed when moving
    float speedSprint = 1.5f; // Multiplier for speed when sprinting

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0.0f || Input.GetAxisRaw("Vertical") != 0.0f)
        {
            // Getting the direction that the player is moving as a normalised vector (only really matters if they are using a controller)
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            direction.Normalize();

            // Turning the player


            // Moving the player
            transform.position += (direction * speedBase * Time.deltaTime);
        }
    }
}
