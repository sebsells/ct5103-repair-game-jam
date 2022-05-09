using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
