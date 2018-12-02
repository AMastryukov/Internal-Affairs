using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    enum GameState { ONGOING, DEFEAT }

    [SerializeField] private GameState state = GameState.ONGOING;
    [SerializeField] private int turn = 0;

    [SerializeField] private int minDefeatThreshold = 0;
    [SerializeField] private int maxDefeatThreshold = 30;

    [SerializeField] private Player player;
    [SerializeField] private Advisor candidate;

    [SerializeField] private FeedbackText feedbackText;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Transform deltaPopups;
    [SerializeField] private GameObject attributeDeltaPrefab;

    public int MinDefeatThreshold
    {
        get { return minDefeatThreshold; }
    }

    public int MaxDefeatThreshold
    {
        get { return maxDefeatThreshold; }
    }

    private void Start()
    {
        UpdateTurnText();
    }

    public void ProcessTurn(Advisor culledAdvisor)
    {
        Vector3 deltaVector;

        if (state == GameState.ONGOING)
        {
            // give a feedback message at random
            feedbackText.RandomizeFeedbackText(culledAdvisor.AdvisorName, candidate.AdvisorName);

            // assign the candidate to the selected advisor
            culledAdvisor.AssignNewAdvisor(candidate);
            deltaVector = CalculateDeltaVector();
            candidate.GenerateNewAdvisor(deltaVector);

            // spawn attribute delta prefab
            GameObject attributeDelta = Instantiate(attributeDeltaPrefab, deltaPopups.position, Quaternion.identity, deltaPopups);
            attributeDelta.GetComponent<AttributeDelta>().AssignAttributes(deltaVector);

            // calculate attributes
            player.CalculateAttributes();

            turn++;
            UpdateTurnText();
            
            CheckGameState();
        }
    }

    private void UpdateTurnText()
    {
        turnText.text = "Turn: " + turn.ToString();
    }

    private Vector3 CalculateDeltaVector()
    {
        Vector3 deltaVector = Vector3.zero;

        // sum all advisor vectors
        for (int i = 0; i < player.Advisors.Count; i++)
        {
            deltaVector += player.Advisors[i].Attributes;
        }

        return deltaVector;
    }

    private void CheckGameState()
    {
        // check if any of the player's attributes are too low or too high
        for (int i = 0; i < 3; i++)
        {
            if (player.Attributes[i] <= minDefeatThreshold || 
                player.Attributes[i] >= maxDefeatThreshold)
            {
                state = GameState.DEFEAT;
            }
        }
    }
}
