using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject m_pauseScreen;
    [SerializeField] private GameObject m_startScreen;
    
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start()
    {
        Time.timeScale = 0;
        m_pauseScreen.SetActive(false);
        m_startScreen.SetActive(true);
    }

    public void StartGame()
    {
        m_startScreen.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void TogglePause()
    {
        bool isPaused = Time.timeScale == 0;
        Time.timeScale = isPaused ? 1 : 0;
        
        m_pauseScreen.SetActive(!isPaused);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
