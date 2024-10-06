using UnityEngine;

public class JointBreakHandler : MonoBehaviour
{
    private AnchorPointHandler m_handler;
    
    private void Awake()
    {
        m_handler = FindObjectOfType<AnchorPointHandler>();
    }
    
    private void OnJointBreak2D(Joint2D brokenJoint)
    {
        m_handler.RemoveSpringJointPair(brokenJoint as SpringJoint2D);
    }
}
