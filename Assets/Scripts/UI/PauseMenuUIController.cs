using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUIController : MonoBehaviour
    {
        private GameManager m_manager;
        
        void OnEnable()
        {
            m_manager = GameManager.Instance;
            
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            Button resumeButton = root.Q<Button>("Resume");
            Button mainMenuButton = root.Q<Button>("MainMenu");

            resumeButton.clicked += OnResume;
            mainMenuButton.clicked += OnMainMenu;
        }

        void OnResume()
        {
            //TODO Return to game
            m_manager.OnPauseGame();
        }
        
        void OnMainMenu()
        {
            //TODO Return to mainmenu
            m_manager.OnMainMenu();
        }
    }
}
