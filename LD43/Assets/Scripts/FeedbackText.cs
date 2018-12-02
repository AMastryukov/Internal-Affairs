using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FeedbackText : MonoBehaviour
{
    [SerializeField] private FeedbackMessages feedbackMessages;
    [SerializeField] private TextMeshProUGUI feedbackText;
    
    public void RandomizeFeedbackText(string oldAdvisor, string newAdvisor)
    {
        string template = feedbackMessages.feedbackMessages[Random.Range(0, feedbackMessages.feedbackMessages.Count)];

        feedbackText.text = string.Format(template, oldAdvisor, newAdvisor);
    }
}
