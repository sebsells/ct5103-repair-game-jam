using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorScript : MonoBehaviour
{
    [SerializeField] Slider slider; // Health bar slider

    [SerializeField] float maxHealth = 100.0f;      // Maximum health of generator
    [SerializeField] float playerHealRate = 10.0f;  // How fast the player can repair the generator
    [SerializeField] float enemyDamageRate = 5.0f;  // How fast enemies damage the generator

    private float currentHealth; // Current amount of health of the generator
    private float currentHealthRate; // Current amount of health being lost/gained by the generator

    private void Start()
    {
        currentHealth = maxHealth/2; // Giving generator max health
        currentHealthRate = 0.0f; // Generator shouldn't be losing or gaining health at this current time
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth += currentHealthRate * Time.deltaTime; // Updating health for any damage/repair
        slider.value = currentHealth / maxHealth; // Updating slider health bar with new health

        // If generator health reaches below 0, then it will destroy itself
        if (currentHealth <= 0.0f)
        {
            Destroy(gameObject);
        }
        // If generator goes above max health, bring it back down
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Called by the player when repairing
    public void Repair(bool repairing)
    {
        if (repairing)
        {
            currentHealthRate += playerHealRate;
        }
        else
        {
            currentHealthRate -= playerHealRate;
        }
    }

    // Generic method for changing the generators health rate
    public void ModifyHealthRate(float rate)
    {
        currentHealthRate += rate;
    }
    // Used by bullets to directly modify the health, instead of over time
    public void ModifyHealth(float health)
    {
        currentHealth += health;
    }

    // Gets called by the trigger script
    // Trigger is a separate entity & script because it caused issues when it was shot by the player
    public void OnGenTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().generator = this;
        }
    }
    public void OnGenTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().generator = null;
        }
    }
}
