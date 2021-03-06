using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject bullet; // Bullet prefab that will be fired
    [SerializeField] GameObject canvas; // Canvas of the player (used here to make sure it doesn't turn with player)
    float bulletCooldown = 0.3f; // Time between each bullet fire
    float bulletLastFired = 0.0f; // Time when bullet was last fired

    bool isAiming = false; // True if the player is aiming with arrow keys or right joystick

    
    public GeneratorScript generator; // Current generator the player is in range of
    GeneratorScript generatorRepairing = null; // Generator currently being repaired by the player
    bool isRepairing = false; // True if the player is holding the repair key (used to prevent movement and guns)

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

        // REPAIRING
        // If player is in range of a generator
        if (generator != null)
        {
            // And player is holding repair
            if (Input.GetAxisRaw("Repair") != 0.0f && !isRepairing)
            {
                generatorRepairing = generator;
                generatorRepairing.Repair(true);
                isRepairing = true;
            }
        }
        // If player is no longer in range of generator or lets go of the key
        if ((Input.GetAxisRaw("Repair") == 0.0f || generator == null) && isRepairing)
        {
            generatorRepairing.Repair(false);
            generatorRepairing = null;
            isRepairing = false;
        }


        // MOVEMENT
        if ((Input.GetAxisRaw("MoveH") != 0.0f || Input.GetAxisRaw("MoveV") != 0.0f) && !isRepairing)
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
            speed = speedBase;
            GetComponent<Rigidbody>().position += directionV3 * speed * Time.deltaTime;
            //transform.Translate(directionV3 * speed * Time.deltaTime, Space.World);
        }

        canvas.transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f); // Making sure canvas isnt rotating with the player
        isAiming = false;
    }
}
