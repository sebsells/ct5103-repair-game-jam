using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Slider slider; // Health bar
    [SerializeField] float maxHealth = 10.0f; // Max player health
    [SerializeField] float enemyDamage; // How much damage the player takes when hit
    [SerializeField] float regenRate = 1.0f; // How fast the player regens health
    [SerializeField] float regenCooldown = 3.0f; // How it takes the player to start regen after being hit

    float lastHitTime; // Time when player was last hit
    float health; // Current player health

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if health has gone above max, and move it back down if so
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        // Check if player is below max hp and has survived long enough to start regen
        if (health < maxHealth && lastHitTime + regenCooldown <= Time.time)
        {
            health += regenRate * Time.deltaTime;
        }

        slider.value = health / maxHealth; // Updating health bar
    }

    public void DamagePlayer()
    {
        health -= enemyDamage; // Dropping player health
        lastHitTime = Time.time; // Resetting regen cooldown
    }

    // Used by gamemanager to see if player is dead
    public float GetHealth()
    {
        return health;
    }
}
