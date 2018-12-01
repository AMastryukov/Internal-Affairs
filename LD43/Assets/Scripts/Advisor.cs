using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Advisor : MonoBehaviour
{
    [SerializeField] private AdvisorNames advisorNames;

    [SerializeField] private string advisorName;
    [SerializeField] private Vector3 attributes;
    [SerializeField] private Vector3 attributeHiddenFlags;
    
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private AttributeText attributeText;

    public string AdvisorName
    {
        get { return advisorName; }
    }

    public Vector3 Attributes
    {
        get { return attributes; }
    }

    private void Start()
    {
        GenerateNewAdvisor(Vector3.zero);
    }

    public void GenerateNewAdvisor(Vector3 deltaVector)
    {
        // generate random advisor name from name list
        advisorName = advisorNames.names[Random.Range(0, advisorNames.names.Count)];
        
        Vector3 newAttributes = Vector3.zero;
        
        // calculate new attributes based on the delta vector
        for (int i = 0; i < 3; i++)
        {
            if (deltaVector[i] < 0)
            {
                newAttributes[i] = Mathf.Clamp(Random.Range(-1, (int)Mathf.Abs(deltaVector[i])), -2, 3);

                // ensure the number is not 0
                if (newAttributes[i] == 0)
                {
                    newAttributes[i] = 1;
                }
            }
            else if (deltaVector[i] == 0)
            {
                // ensure the number is not 0
                while (newAttributes[i] == 0)
                {
                    newAttributes[i] = Random.Range(-2, 3);
                }
            }
            else
            {
                newAttributes[i] = Mathf.Clamp(Random.Range((int)-(deltaVector[i]), 2), -2, 3);

                // ensure the number is not 0
                if (newAttributes[i] == 0)
                {
                    newAttributes[i] = -1;
                }
            }
        }

        attributeHiddenFlags = Vector3.zero;

        // generate attribute hidden flags
        if (Random.Range(0f, 1f) < 0.6f)
        {
            attributeHiddenFlags = new Vector3(
                Mathf.Clamp01(Random.Range(-3, 2)),
                Mathf.Clamp01(Random.Range(-3, 2)),
                Mathf.Clamp01(Random.Range(-3, 2))
                );
        }

        attributes = newAttributes;

        UpdateTexts();
    }

    public void AssignNewAdvisor(Advisor advisor)
    {
        advisorName = advisor.advisorName;
        attributes = advisor.attributes;
        attributeHiddenFlags = advisor.attributeHiddenFlags;

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        nameText.text = advisorName;
        attributeText.UpdateAttributeTexts(attributes, attributeHiddenFlags);
    }
}
