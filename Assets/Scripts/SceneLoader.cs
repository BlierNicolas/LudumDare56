using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
    
    public void LoadGame()
    {
        Application.LoadLevel("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
