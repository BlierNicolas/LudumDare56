using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    { 
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject m_pauseScreen;

        public float m_score { get; private set; } = 0f;
    
        void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            }

            m_score = 1000f;
        }

        void Start()
        {
            Time.timeScale = 1;
            m_pauseScreen.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPauseGame();
            }
        }
        
        public void OnPauseGame()
        {
            bool isPaused = Time.timeScale == 0;
            Time.timeScale = isPaused ? 1 : 0;

            m_pauseScreen.SetActive(!m_pauseScreen.gameObject.activeSelf);
        }

        public void OnMainMenu()
        {
            Debug.Log("Loading");
            SceneManager.LoadSceneAsync("MainMenuPrototype");
        }
    }
}
