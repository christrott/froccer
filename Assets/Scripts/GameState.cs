using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    PLAYING,
    GOAL,
    OOB,
    RUSH,
    OVERTIME
}

public class GameState : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public States gameState;
    
    private float _timer;

    public float Timer
    {
        get {
            return _timer;
        }
    }

	// Use this for initialization
	void Start()
    {
        _timer = 300.0f;
        gameState = States.PLAYING;
	}

    void Update()
    {
        if (gameState == States.PLAYING)
        {
            if (_timer < 5.0f)
            {
                _timer -= Time.deltaTime * 0.75f;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }
    }

    public void UpdateState(States newState)
    {
        States oldState = gameState;
        gameState = newState;

        switch (newState)
        {
            case States.PLAYING:
                break;
            case States.GOAL:
                GoalScored();
                break;
        }
    }

    private void GoalScored()
    {
        ResetPlayers();
        // Do some reset fanciness
        // Display score
        UpdateState(States.PLAYING);
    }

    private void ResetPlayers()
    {
        player1.GetComponent<PlayerController>().ResetPosition();
        player2.GetComponent<PlayerController>().ResetPosition();
    }
}
