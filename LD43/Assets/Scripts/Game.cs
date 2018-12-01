using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    enum GameState { INPROGRESS, VICTORY, DEFEAT }

    [SerializeField] private GameState state = GameState.INPROGRESS;
    [SerializeField] private float turnInterval = 2f;
    [SerializeField] private int turn = 0;

    [SerializeField] private Player player;
    [SerializeField] private Advisor candidate;

    [SerializeField] private TextMeshProUGUI turnText;

    private void Start()
    {
        UpdateTurnText();
    }

    public void ProcessTurn(Advisor culledAdvisor)
    {
        // assign the candidate to the selected advisor
        culledAdvisor.AssignNewAdvisor(candidate);
        candidate.GenerateNewAdvisor(CalculateDeltaVector());

        // calculate attributes
        player.CalculateAttributes();

        turn++;
        UpdateTurnText();
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
            deltaVector += player.Advisors[0].Attributes;
        }

        return deltaVector;
    }
}
