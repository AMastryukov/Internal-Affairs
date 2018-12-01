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

    // UI elements
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI loyaltyText;
    [SerializeField] private TextMeshProUGUI mightText;
    [SerializeField] private TextMeshProUGUI influenceText;
    [SerializeField] private Color32 positiveColor;
    [SerializeField] private Color32 neutralColor;
    [SerializeField] private Color32 negativeColor;

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
        
        // keep generating attribute vector until its sum is the handicap factor
        while (newAttributes[0] == 0 || newAttributes[1] == 0 || newAttributes[2] == 0 || 
            (newAttributes[0] + newAttributes[1] + newAttributes[2] > 1) ||
            (newAttributes[0] + newAttributes[1] + newAttributes[2] < -1))
        {

            // calculate new attributes based on the delta vector
            for (int i = 0; i < 3; i++)
            {
                if (deltaVector[i] < 0)
                {
                    newAttributes[i] = Mathf.Clamp(Random.Range(-1, Mathf.Abs(deltaVector[i])), -2, 3);
                }
                else if (deltaVector[i] == 0)
                {
                    newAttributes[i] = Random.Range(-2, 3);
                }
                else
                {
                    newAttributes[i] = Mathf.Clamp(Random.Range(-(deltaVector[i]), 2), -2, 3);
                }
            }
        }

        attributeHiddenFlags = Vector3.zero;

        // generate attribute hidden flags
        if (Random.Range(0, 3) == 0)
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

        // display pluses or minuses instead of actual numbers
        UpdateAttributeText(loyaltyText, attributes[0], attributeHiddenFlags[0] == 1 ? true : false);
        UpdateAttributeText(mightText, attributes[1], attributeHiddenFlags[1] == 1 ? true : false);
        UpdateAttributeText(influenceText, attributes[2], attributeHiddenFlags[2] == 1 ? true : false);
    }

    private void UpdateAttributeText(TextMeshProUGUI text, float number, bool hidden)
    {
        // if the attribute is hidden, don't bother doing anything else
        if (hidden)
        {
            text.text = "?";
            text.color = neutralColor;

            return;
        }

        // assign the sign character to the sign of the number
        char sign = Mathf.Sign(number) > 0 ? '+' : '-';

        // set the text color
        if (sign == '+')
        {
            text.color = positiveColor;
        }
        else
        {
            text.color = negativeColor;
        }

        text.text = "";

        // add appropriate sign according to the number
        for (int i = 0; i < Mathf.Abs(number); i++)
        {
            text.text += sign;
        }
    }
}
