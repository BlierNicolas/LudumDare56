using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class GameOverUIController : MonoBehaviour
    {
        private GameManager m_gameManager;
        private bool m_victory = false;
        
        private void OnEnable()
        {
            m_gameManager = GameManager.Instance;
            
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            Label gameOverLabel = root.Q<Label>("GameOverText");
            Button mainMenuButton = root.Q<Button>("MainMenu");

            //TODO Get gameover status from gamemanager
            
            if (m_victory)
            {
                gameOverLabel.text = "Victory";
            }
            else
            {
                gameOverLabel.text = "Defeat";
            }
            
            mainMenuButton.clicked += OnMainMenu;
        }

        private void OnMainMenu()
        {
            //TODO Change scene to main menu
            m_gameManager.OnMainMenu();
        }
    }
}
