using System.Collections.Generic;
using UnityEngine;

public class FeatureGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_featurePoints = new();
    [SerializeField] private List<GameObject> m_anchorPoints = new();

    public int GetFeatureAmount()
    {
        return GetAmount(m_featurePoints);
    }

    public int GetAnchorAmount()
    {
        return GetAmount(m_anchorPoints);
    }

    private int GetAmount(List<GameObject> objects)
    {
        if (objects == null || objects.Count == 0) return 0;

        return objects.Count;
    }

    public void AssignFeature(List<GameObject> features)
    {
        AssignGameObject(features, m_featurePoints);
    }

    public void AssignTendrils(List<GameObject> tendrils)
    {
        AssignGameObject(tendrils, m_anchorPoints);
    }

    private void AssignGameObject(List<GameObject> prefabs, List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (prefabs[i] != null)
            {
                Instantiate(prefabs[i], objects[i].transform);
            }
        }
    }
}
