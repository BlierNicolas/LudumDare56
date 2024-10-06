using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class InGameUIController : MonoBehaviour
    {
        private Label m_scoreLabel;
        private Label m_unmberUsedLabel;
        private float m_score = 0f;
        private float m_numberUsed = 0f;
    
        void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;

            m_scoreLabel = root.Q<Label>("Score");
        }

        void Update()
        {
            //TODO Get score from gamemanager
            //TODO Get numberused from gamemanager
            
            m_scoreLabel.text = ((int)m_score).ToString();
            m_unmberUsedLabel.text = ((int)m_numberUsed).ToString();
        }
    }
}
