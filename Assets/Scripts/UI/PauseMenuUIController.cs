using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUIController : MonoBehaviour
    {
        private GameManager m_manager;
        
        private void OnEnable()
        {
            m_manager = GameManager.Instance;
            
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            Button resumeButton = root.Q<Button>("Resume");
            Button mainMenuButton = root.Q<Button>("MainMenu");

            resumeButton.clicked += OnResume;
            mainMenuButton.clicked += OnMainMenu;
        }

        private void OnResume()
        {
            m_manager.OnPauseGame();
        }
        
        private void OnMainMenu()
        {
            m_manager.OnMainMenu();
        }
    }
}
