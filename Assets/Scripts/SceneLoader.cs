using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
    
    public void LoadGame()
    {
        SceneManager.LoadSceneAsync("InGame with Sound");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
