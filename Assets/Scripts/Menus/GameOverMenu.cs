using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] Text killsText; // Text boxes for stats
    [SerializeField] Text timeText;

    string kills; // Total kills player got
    string time; // Total time player lasted

    private void Start()
    {
        // Getting players performance from playerprefs
        kills = PlayerPrefs.GetString("kills");
        time = PlayerPrefs.GetString("time");

        Debug.Log(kills + " " + time);

        // Updating score text boxes
        killsText.text = kills;
        timeText.text = time;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
