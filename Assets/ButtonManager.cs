using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool isPaused;
    public void OnButtonPressedLoadLevel(string LevelToLoadName)
    {
        SceneManager.LoadScene(LevelToLoadName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
