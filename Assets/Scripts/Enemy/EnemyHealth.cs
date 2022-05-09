using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] Slider slider; // Health bar
    [SerializeField] float maxHealth = 10.0f; // Max health of enemy
    [SerializeField] float playerDamage = 5.0f; // How much damage the enemy takes when shot
    float health; // Current health

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Setting max health
    }

    // Update is called once per frame
    void Update()
    {
        // Updating health bar
        slider.value = health / maxHealth;

        // If health reaches 0 then die
        if (health <= 0.0f)
        {
            // Stop damaging generator
            GetComponent<EnemyMovement>().StopAttacking();

            // Looking for the game manager, to notify it that an enemy has died
            GameObject[] managerLookup = GameObject.FindGameObjectsWithTag("GameManager"); // Finding manager
            managerLookup[0].GetComponent<GameManager>().EnemyKill(); // Notifying it

            // Destroy self
            Destroy(gameObject);
        }
    }

    // Used by bullets to damage enemies
    public void Damage()
    {
        health -= playerDamage; // Deducting health from self
    }
}
