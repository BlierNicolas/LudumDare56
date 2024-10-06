using System;
using System.Collections.Generic;
using UnityEngine;

public class AnchorPointHandler : MonoBehaviour
{
    [SerializeField] private float m_jointPullForce = 4.0f;
    [SerializeField] private float m_jointMinimalDistance = 0.005f;
    [SerializeField] private float m_jointBreakForce = 5.0f;
    [SerializeField] private float m_connectionDistance = 0.1f;
    
    private readonly List<GameObject> m_anchorPoints = new();
    private Dictionary<SpringJoint2D, Tuple<Transform, Transform>> m_jointAnchors = new();
    
    public void RegisterGameObject(GameObject anchorPoint)
    {
        if(!m_anchorPoints.Contains(anchorPoint.gameObject))
            m_anchorPoints.Add(anchorPoint.gameObject);
    }

    public void UnregisterGameObject(GameObject anchorPoint)
    {
        if (m_anchorPoints.Contains(anchorPoint.gameObject))
            m_anchorPoints.Remove(anchorPoint.gameObject);
    }
    
    private void Update()
    {
        CheckAnchorPointsOverlap();
    }

    private void UpdateAnchorPointStatus(Transform anchor, bool connected)
    {
        var anchorPoint = anchor.GetComponent<AnchorPoint>();

        if (anchorPoint != null)
        {
            anchorPoint.isConnected = connected;
        }
    }

    private void CheckAnchorPointsOverlap()
    {
        for (int i = 0; i < m_anchorPoints.Count - 1; i++)
        {
            for (int j = i + 1; j < m_anchorPoints.Count; j++)
            {
                if (Vector2.Distance(m_anchorPoints[i].transform.position, m_anchorPoints[j].transform.position) < m_connectionDistance)
                {
                    CreateSpringJointPair(m_anchorPoints[i],m_anchorPoints[j]);
                    
                    m_anchorPoints.RemoveAt(j);
                    m_anchorPoints.RemoveAt(i);
                    
                    i--;
                    break;
                }
            }
        }
    }

    private void CreateSpringJointPair(GameObject effector, GameObject affected)
    {
        var shapeEffector = effector.transform.parent.parent.gameObject;
        
        var joint = shapeEffector.AddComponent<SpringJoint2D>();
        
        var connected = affected.transform.parent.parent.GetComponent<Rigidbody2D>();
        
        joint.connectedBody = connected;
        
        joint.enableCollision = true;
        joint.anchor = effector.transform.localPosition;
        joint.connectedAnchor = affected.transform.localPosition;
        joint.frequency = m_jointPullForce;
        joint.distance = m_jointMinimalDistance;
        joint.breakForce = m_jointBreakForce;
        
        m_jointAnchors[joint] = new Tuple<Transform, Transform>(effector.transform, affected.transform);

        UpdateAnchorPointStatus(m_jointAnchors[joint].Item1, true);
        UpdateAnchorPointStatus(m_jointAnchors[joint].Item2, true);
        
        print("Joint created between : " + effector.name + " and " + affected.name);
    }

    public void RemoveSpringJointPair(SpringJoint2D brokenJoint)
    {
        if (m_jointAnchors.TryGetValue(brokenJoint, out var anchors))
        {
            m_anchorPoints.Add(anchors.Item1.gameObject);
            m_anchorPoints.Add(anchors.Item2.gameObject);

            UpdateAnchorPointStatus(anchors.Item1, false);
            UpdateAnchorPointStatus(anchors.Item2, false);
            
            m_jointAnchors.Remove(brokenJoint);

            print("Joint destroyed between : " + anchors.Item1.gameObject.name + " and " +
                  anchors.Item2.gameObject.name + " forces:" + brokenJoint.breakForce + " " + brokenJoint.breakTorque);
        }
    }
}
