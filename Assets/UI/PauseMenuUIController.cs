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

            resumeButton.clicked += OnResume;
        }

        void OnResume()
        {
            //TODO
            //Return to game
        }
    }
}
