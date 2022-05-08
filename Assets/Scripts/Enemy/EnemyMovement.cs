using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Enemy has radius around it and searches for the nearest player/generator
    // Player radius is smaller

    // Enemy prioritises player if both are in range

    // If enemy reaches gen (GenCloseDetection), it will stop moving completely
    // If player gets very close then the enemy will start chasing them

    [SerializeField] float speed = 5.0f; // How fast the enemy travels

    bool isAttackingGen; // True if the enemy has reached a generator
    Vector3 targetPos; // Where the enemy is currently headed towards
    GameObject trackedPlayer; // GO of the player if they are in range
    GameObject trackedGen; // GO of the generator being tracked and/or attacked
    List<GameObject> generatorsInRange; // List of all gens in range

    private void Start()
    {
        isAttackingGen = false;
        targetPos = Vector3.zero;
        trackedPlayer = null;
        trackedGen = null;
        generatorsInRange = new List<GameObject>();
    }

    private void Update()
    {
        // If the enemy is tracking the player, make them the target position
        if (trackedPlayer != null)
        {
            targetPos = trackedPlayer.transform.position;
        }

        // If the enemy is tracking generators, make the closest one the target
        else if (generatorsInRange.Count >= 1)
        {
            // Checking when gen is closest
            GameObject closestGen = null;
            float distance = 9999.0f;
            for (int i = 0; i < generatorsInRange.Count; ++i)
            {
                if (distance > (transform.position - generatorsInRange[i].transform.position).magnitude)
                {
                    distance = (transform.position - generatorsInRange[i].transform.position).magnitude;
                    closestGen = generatorsInRange[i];
                }
            }
            trackedGen = closestGen;

            // Closest gen becomes targer
            targetPos = trackedGen.transform.position;
        }

        // If the enemy is tracking nothing, walk to centre of the map
        else
        {
            targetPos = Vector3.up; // (0,1,0)
        }

        // Moving the enemy (if it isnt attacking the gen)
        if (!isAttackingGen || trackedPlayer != null)
        {
            Vector3 directionV3 = (targetPos - transform.position).normalized;
            transform.Translate(directionV3 * speed * Time.deltaTime);
        }
    }

    // Called by triggers when something enters/exits their radius
    public void OnRadiusEnter(string name, Collider other)
    {
        // Checking for nearby player when attacking the gen
        // This overrides the attacking gen flag
        if (name == "PlayerCloseDetection" && other.tag == "Player" && isAttackingGen)
        {
            Debug.Log("stopping attack");
            trackedPlayer = other.gameObject; // Start tracking player
            trackedGen.GetComponent<GeneratorScript>().EnemyAttack(false); // Stop damaging gen
            return;
        }

        // If the enemy is not currently attacking a generator, it will search for one (or the player) to attack
        if (!isAttackingGen)
        {
            // Checking if enemy has reached the generator
            if (name == "GenCloseDetection" && other.tag == "Generator")
            {
                Debug.Log("starting attack");
                trackedPlayer = null; // Stop tracking player
                other.GetComponent<GeneratorScript>().EnemyAttack(true); // Start damaging the generator
                isAttackingGen = true; // Set attacking gen to true, so the enemy will stop tracking gens/the player
                return;
            }

            // Checking for nearby player/generator
            if (name == "PlayerDetection" && other.tag == "Player")
            {
                trackedPlayer = other.gameObject;
            }
            else if (name == "GenDetection" && other.tag == "Generator")
            {
                // Update() will check for closest gen and set target to there
                generatorsInRange.Add(other.gameObject);
            }
        }
    }

    public void OnRadiusExit(string name, Collider other)
    {
        if (name == "GenDetection" && other.tag == "Generator")
        {
            generatorsInRange.Remove(other.gameObject);
        }
        if (name == "PlayerDetection" && other.tag == "Player")
        {
            trackedPlayer = null;
        }
        if (name == "GenCloseDetection" && other.tag == "Generator")
        {
            isAttackingGen = false;
        }
    }

    // Called by the health script when the enemy is killed
    public void StopAttacking()
    {
        if (trackedGen != null && isAttackingGen)
        {
            trackedGen.GetComponent<GeneratorScript>().EnemyAttack(false);
        }
    }
}
