using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    enum GameState { ONGOING, DEFEAT }

    [SerializeField] private GameState state = GameState.ONGOING;
    [SerializeField] private int turn = 0;

    [SerializeField] private Player player;
    [SerializeField] private Advisor candidate;

    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private Transform deltaPopups;
    [SerializeField] private GameObject attributeDeltaPrefab;

    private void Start()
    {
        UpdateTurnText();
    }

    public void ProcessTurn(Advisor culledAdvisor)
    {
        if (state == GameState.ONGOING)
        {
            // assign the candidate to the selected advisor
            culledAdvisor.AssignNewAdvisor(candidate);
            candidate.GenerateNewAdvisor(CalculateDeltaVector());

            // spawn attribute delta prefab
            GameObject attributeDelta = Instantiate(attributeDeltaPrefab, deltaPopups.position, Quaternion.identity, deltaPopups);
            attributeDelta.GetComponent<AttributeDelta>().AssignAttributes(CalculateDeltaVector());

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
        // check if any of the player's attributes are 0
        for (int i = 0; i < 3; i++)
        {
            if (player.Attributes[i] <= 0)
            {
                state = GameState.DEFEAT;

                Debug.Log("DEFEATED");
            }
        }
    }
}
