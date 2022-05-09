using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy; // Enemy prefab for the spawners to use
    [SerializeField] int wave0Enemy = 3; // Amount of enemies on wave 0
    [SerializeField] int waveIncrease = 1; // Amount of extra enemies per round
    [SerializeField] float spawnDelay = 1.0f; // Time between each enemy spawn

    int currentRound = -1; // Current wave the player is on
    int enemyTotal = 0; // Total enemy spawned in this wave
    int enemyRemaining = 0; // Enemies currently remaining for this wave
    int enemyToSpawn = 0; // Enemies left to spawn for this wave

    float timeSinceLastSpawn; // Time of last enemy spawn

    List<GameObject> spawners; // List of all spawners

    // Start is called before the first frame update
    void Start()
    {
        // Getting each child and adding it to the spawner list
        spawners = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if (child.tag == "EnemySpawn")
            {
                spawners.Add(child.gameObject);
            }
        }

        // Starting first round
        timeSinceLastSpawn = Time.time;
        NewRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyRemaining <= 0)
        {
            NewRound();
        }

        // Enough time has passed to spawn a new enemy (if required)
        if (timeSinceLastSpawn + spawnDelay <= Time.time && enemyToSpawn > 0)
        {
            // Resetting cooldown and decreasing remaining enemies left to spawn
            timeSinceLastSpawn = Time.time;
            enemyToSpawn--;

            // Spawning enemy
            Instantiate(enemy, spawners[Random.Range(0, spawners.Count - 1)].transform);
        }
    }

    void NewRound() // Sets up new round
    {
        currentRound++; // Increasing round
        enemyTotal = wave0Enemy + (waveIncrease * currentRound); // Setting total enemies for this wave
        enemyRemaining = enemyTotal; // Resetting enemies remaining
        enemyToSpawn = enemyTotal; // Resetting enemies to spawn
    }

    public void EnemyKilled()
    {
        enemyRemaining--;
    }
}
