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

        [SerializeField] private GameObject m_spawnPoint;
        [SerializeField] private GameObject m_lockedItems;

        public float m_score { get; private set; } = 0f;
    
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

            m_score = 1000f;
        }

        private void Start()
        {
            Time.timeScale = 1;
            m_pauseScreen.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPauseGame();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (m_spawnPoint.transform.childCount > 0)
                {
                    var child = m_spawnPoint.transform.GetChild(0);
                    Destroy(child.GetComponent<CharacterStateMachine>());
                    m_spawnPoint.transform.DetachChildren();
                    child.transform.SetParent(m_lockedItems.transform, false);
                }
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
