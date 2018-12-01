using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private Vector3 attributes;
    [SerializeField] private List<Advisor> advisors;

    // UI Elements
    [SerializeField] private TextMeshProUGUI loyaltyText;
    [SerializeField] private TextMeshProUGUI mightText;
    [SerializeField] private TextMeshProUGUI influenceText;

    public Vector3 Attributes
    {
        get { return attributes; }
    }

    public List<Advisor> Advisors
    {
        get { return advisors; }
    }

    private void Start()
    {
        loyaltyText.text = ((int)attributes[0]).ToString();
        mightText.text = ((int)attributes[1]).ToString();
        influenceText.text = ((int)attributes[2]).ToString();
    }

    public void CalculateAttributes()
    {
        Vector3 oldValues = new Vector3(attributes[0], attributes[1], attributes[2]);

        // go through all advisors and calculate new attributes
        for (int i = 0; i < advisors.Count; i++)
        {
            attributes[0] += advisors[i].Attributes[0];
            attributes[1] += advisors[i].Attributes[1];
            attributes[2] += advisors[i].Attributes[2];
        }

        UpdateTexts(oldValues);
    }

    private void UpdateTexts(Vector3 oldValues)
    {
        StartCoroutine(AnimateAttribute(loyaltyText, oldValues[0], attributes[0]));
        StartCoroutine(AnimateAttribute(mightText, oldValues[1], attributes[1]));
        StartCoroutine(AnimateAttribute(influenceText, oldValues[2], attributes[2]));
    }
    
    private IEnumerator AnimateAttribute(TextMeshProUGUI text, float oldValue, float newValue)
    {
        int currentValue = (int)oldValue;
        int delta = (int)Mathf.Sign(newValue - oldValue);

        // do a slot-machine like counter increase on the attribute
        for (int i = 0; i < Mathf.Abs(newValue - oldValue); i++)
        {
            text.text = currentValue.ToString();
            currentValue += delta;

            yield return new WaitForSeconds(0.25f);
        }
    }
}
