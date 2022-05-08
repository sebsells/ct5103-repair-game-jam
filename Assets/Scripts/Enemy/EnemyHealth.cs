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

        // If health reaches 0 then destroy self
        if (health <= 0.0f)
        {
            GetComponent<EnemyMovement>().StopAttacking(); // Stop damaging generator
            Destroy(gameObject); // Destroy self
        }
    }

    // Used by bullets to damage enemies
    public void Damage()
    {
        health -= playerDamage; // Deducting health from self
    }
}
