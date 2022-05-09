using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;

    float startTime; // Time the game started
    int totalKills; // How many kills the player has

    private void Start()
    {
        startTime = Time.time; // Time the game started
        totalKills = 0; // Initial kill count is 0
    }

    private void Update()
    {
        // Checking for all generators
        GameObject[] generators = GameObject.FindGameObjectsWithTag("Generator");
        if (generators.Length == 0)
        {
            // No generators left, end round
            EndRound();
        }

        // Checking player health
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        if (player[0].GetComponent<PlayerHealth>() != null)
        {
            if (player[0].GetComponent<PlayerHealth>().GetHealth() <= 0.0f)
            {
                // Player has died, end round
                EndRound();
            }
        }
    }

    // Ends the game and takes the player to the game over screen
    private void EndRound() {
        // Turning total play time into a string (mm:ss)
        float playTime = Time.time - startTime;
        string minutes = ((int)(playTime / 60)).ToString();
        string seconds;
        if (playTime % 60 < 10)
        {
            seconds = "0" + ((int)(playTime % 60)).ToString(); // Adding extra an zero, so time doesn't look like "1:4" instead of "1:04"
        }
        else
        {
            seconds = ((int)(playTime % 60)).ToString();
        }
        string timeString = minutes + ":" + seconds;

        // Setting player prefs so the gameover screen knows the player's stats
        PlayerPrefs.SetString("kills", totalKills.ToString());
        PlayerPrefs.SetString("time", timeString);

        Debug.Log(PlayerPrefs.GetString("kills"));

        // Swapping over to the gameover screen
        SceneManager.LoadScene(3);
    }

    // Called by an enemy when it dies
    // Notifies the spawner, so a new round can (potentially start)
    // Also increases kill counter for game over screen
    public void EnemyKill()
    {
        spawner.EnemyKilled(); // Notifiying spawner
        totalKills++; // Increasing kill count
    }
}
