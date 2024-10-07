using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class TetraminosGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_shapes = new();
    [SerializeField] private List<GameObject> m_eyes = new();
    [SerializeField] private List<GameObject> m_mouths = new();
    [SerializeField] private List<GameObject> m_tendrils = new();

    public GameObject m_gary;
    public GameObject m_currentTetramino;
    [SerializeField] private GameObject m_tetraminoList;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            GenerateTetramino();
        }
    }

    public void GenerateTetramino()
    {
        var index = Random.Range(0, m_shapes.Count);
        m_currentTetramino = Instantiate(m_shapes[index], m_tetraminoList.transform);

        var featureGenerator = m_currentTetramino.GetComponent<FeatureGenerator>();

        GenerateFeatures(featureGenerator);
        GenerateTendrils(featureGenerator);
    }

    private int PickNumber(List<int> numbers)
    {
        var index = Random.Range(0, numbers.Count);
        var number = numbers[index];

        numbers.RemoveAt(index);

        return number;
    }

    private void GenerateFeatures(FeatureGenerator featureGenerator)
    {
        var featureAmount = featureGenerator.GetFeatureAmount();
        List<GameObject> featureList = new();

        var discard = Enumerable.Range(0, featureAmount).ToList();

        int eyeOne = PickNumber(discard);
        int eyeTwo = PickNumber(discard);
        int mouth = PickNumber(discard);

        //eyeOneTwoBreakFree //Fou rire de minuit XD

        for (int i = 0; i < featureAmount; i++)
        {
            if (i == eyeOne || i == eyeTwo)
            {
                featureList.Add(m_eyes[Random.Range(0, m_eyes.Count)]);
            }
            else if (i == mouth)
            {
                featureList.Add(m_mouths[Random.Range(0, m_mouths.Count)]);
            }
            else
            {
                featureList.Add(null);
            }
        }

        featureGenerator.AssignFeature(featureList);
    }

    private void GenerateTendrils(FeatureGenerator featureGenerator)
    {
        var tendrilsAmount = featureGenerator.GetAnchorAmount();
        List<GameObject> tendrilList = new();

        for (int i = 0; i < tendrilsAmount; i++)
        {
            tendrilList.Add(m_tendrils[Random.Range(0, m_tendrils.Count - 1)]);
        }

        featureGenerator.AssignTendrils(tendrilList);
    }

    public void SpawnGary()
    {
        var garry = Instantiate(m_gary, m_tetraminoList.transform);

        m_currentTetramino = garry;
    }
}
