using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class InGameUIController : MonoBehaviour
    {
        private Label m_scoreLabel;
        private float m_score = 0f;
    
        void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            m_scoreLabel = root.Q<Label>("Score");
        }

        void Update()
        {
            m_score += Time.deltaTime;
            m_scoreLabel.text = ((int)m_score).ToString();
        }
    }
}
