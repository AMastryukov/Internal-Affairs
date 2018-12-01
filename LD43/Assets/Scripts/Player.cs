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

    public void CalculateAttributes()
    {
        // go through all advisors and calculate new attributes
        for (int i = 0; i < advisors.Count; i++)
        {
            attributes[0] += advisors[i].Attributes[0];
            attributes[1] += advisors[i].Attributes[1];
            attributes[2] += advisors[i].Attributes[2];
        }

        UpdateTexts();
    }

    private void UpdateTexts()
    {
        loyaltyText.text = ((int)attributes[0]).ToString();
        mightText.text = ((int)attributes[1]).ToString();
        influenceText.text = ((int)attributes[2]).ToString();
    }
}
