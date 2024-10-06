using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    private AnchorPointHandler m_handler;

    public bool isConnected = false;
    
    private void Awake()
    {
        m_handler = FindObjectOfType<AnchorPointHandler>();
    }

    private void OnEnable()
    {
        m_handler.RegisterGameObject(gameObject);
    }

    private void OnDisable()
    {
        m_handler.UnregisterGameObject(gameObject);
    }

    private void OnDrawGizmos()
    {
        var icon = isConnected ? "sv_icon_dot3_pix16_gizmo" : "sv_icon_dot6_pix16_gizmo";

        Gizmos.DrawIcon(transform.position, icon, true);
    }
}
