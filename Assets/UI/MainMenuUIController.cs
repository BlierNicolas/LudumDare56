using UnityEngine;
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

            playButton.clicked += OnPlay;
            quitButton.clicked += OnQuit;
        }

        void OnPlay()
        {
            
        }
        
        void OnQuit()
        {
            Application.Quit();
        }
    }
}