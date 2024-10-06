using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            Button playButton = root.Q<Button>("Play");
            Button quitButton = root.Q<Button>("Quit");

            playButton.clicked += OnStartGame;
            quitButton.clicked += OnQuitGame;
        }
        
        void OnStartGame()
        {
            Debug.Log("Loading");
            SceneManager.LoadSceneAsync("LevelPrototype");
        }
        
        void OnQuitGame()
        {
            Application.Quit();
        }
    }
}