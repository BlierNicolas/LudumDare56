using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUIController : MonoBehaviour
    {
        void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            Button resumeButton = root.Q<Button>("Resume");
            Button mainMenuButton = root.Q<Button>("MainMenu");

            resumeButton.clicked += OnResume;
            mainMenuButton.clicked += OnMainMenu;
        }

        void OnResume()
        {
            //TODO Return to game
        }
        
        void OnMainMenu()
        {
            //TODO Return to mainmenu
        }
    }
}
