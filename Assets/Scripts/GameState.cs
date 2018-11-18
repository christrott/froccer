using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    PLAYING,
    GOAL,
    RUSH,
    OVERTIME
}

public class GameState : MonoBehaviour {
    States gameState;
    private float _timer;

    public float Timer
    {
        get {
            return _timer;
        }
    }

	// Use this for initialization
	void Start () {
        gameState = States.PLAYING;
	}
	
	public void UpdateState(States newState)
    {
        switch (newState)
        {
            case States.PLAYING:
                // Resume Timer
                break;
            case States.GOAL:
                ResetPlayState();
                break;
        }
        gameState = newState;
    }

    private void ResetPlayState()
    {

    }
}
