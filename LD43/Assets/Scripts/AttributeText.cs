using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loyaltyText;
    [SerializeField] private TextMeshProUGUI mightText;
    [SerializeField] private TextMeshProUGUI influenceText;

    [SerializeField] private Color32 positiveColor;
    [SerializeField] private Color32 neutralColor;
    [SerializeField] private Color32 negativeColor;

    public TextMeshProUGUI Loyalty
    {
        get { return loyaltyText; }
    }

    public TextMeshProUGUI Might
    {
        get { return mightText; }
    }

    public TextMeshProUGUI Influence
    {
        get { return influenceText; }
    }

    public void UpdateAttributeTexts(Vector3 attributes, Vector3 attributeHiddenFlags)
    {
        UpdateAttributeText(Loyalty, attributes[0], attributeHiddenFlags[0] == 1 ? true : false);
        UpdateAttributeText(Might, attributes[1], attributeHiddenFlags[1] == 1 ? true : false);
        UpdateAttributeText(Influence, attributes[2], attributeHiddenFlags[2] == 1 ? true : false);
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
