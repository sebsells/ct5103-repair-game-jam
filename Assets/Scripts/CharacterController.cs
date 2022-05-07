using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject bullet; // Bullet prefab that will be fired
    float bulletCooldown = 0.3f; // Time between each bullet fire
    float bulletLastFired = 0.0f; // Time when bullet was last fired

    bool isAiming = false; // True if the player is aiming with arrow keys or right joystick

    float speed = 0.0f;       // Calculated speed of the player
    float speedBase = 10.0f;  // Default speed when moving
    float speedSprint = 1.5f; // Multiplier for speed when sprinting

    // Update is called once per frame
    void Update()
    {
        // AIMING
        if (Input.GetAxisRaw("AimH") != 0.0f || Input.GetAxisRaw("AimV") != 0.0f)
        {
            isAiming = true;

            float angle = Mathf.Atan2(Input.GetAxisRaw("AimH"), Input.GetAxisRaw("AimV")) * Mathf.Rad2Deg;
            transform.Rotate(new Vector3(0.0f, angle, 0.0f) - transform.rotation.eulerAngles);
        }

        // FIRING
        if (Input.GetAxisRaw("Fire") != 0.0f)
        {
            if (bulletLastFired + bulletCooldown <= Time.time)
            {
                Vector3 bulletStartPos = new Vector3(Input.GetAxisRaw("AimH"), 0.0f, Input.GetAxisRaw("AimV")) * 0.5f; // Getting the position of the players "gun"
                Instantiate(bullet, transform.position + bulletStartPos, transform.rotation); // Instantiating bullet
                bulletLastFired = Time.time; // Resetting bullet timer
            }
        }

        // MOVEMENT
        if (Input.GetAxisRaw("MoveH") != 0.0f || Input.GetAxisRaw("MoveV") != 0.0f)
        {
            // Getting the direction that the player is moving as a normalised vector (only really matters if they are using a controller)
            Vector3 directionV3 = new Vector3(Input.GetAxisRaw("MoveH"), 0.0f, Input.GetAxisRaw("MoveV"));
            directionV3.Normalize();

            // Turning the player to the direction they are moving (if not aiming)
            if (!isAiming)
            {
                float angle = Mathf.Atan2(Input.GetAxisRaw("MoveH"), Input.GetAxisRaw("MoveV")) * Mathf.Rad2Deg;
                transform.Rotate(new Vector3(0.0f, angle, 0.0f) - transform.rotation.eulerAngles);
            }


            // Moving the player
            transform.Translate(directionV3 * speedBase * Time.deltaTime, Space.World);
        }

        isAiming = false;
    }
}
