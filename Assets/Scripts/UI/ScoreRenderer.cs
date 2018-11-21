using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRenderer : MonoBehaviour {
    public Team scoreTeam;
    private GameState gameState;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
    }

    private void OnGUI()
    {
        float score = 0.0f;
        if (scoreTeam == Team.Team1)
        {
            score = gameState.Team1Score;
        }
        else if (scoreTeam == Team.Team2)
        {
            score = gameState.Team2Score;
        }
        GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }
}
