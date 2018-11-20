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
    public GameObject ball;
    public States gameState;

    public Vector2 centreCircle;


    private float _timer;
    private float resetTimer;

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
        centreCircle = ball.transform.position;
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

    public void UpdateState(States newState, GameObject stateSubject)
    {
        States oldState = gameState;
        gameState = newState;

        switch (newState)
        {
            case States.PLAYING:
                break;
            case States.GOAL:
                if (oldState == States.PLAYING)
                {
                    StartCoroutine("ResetField", stateSubject);
                    GoalScored();
                }
                break;
            case States.OOB:
                if (oldState == States.PLAYING) {
                    string playerSide = stateSubject.GetComponent<PlayerController>().name;
                    resetTimer = 0.25f;
                    StartCoroutine("OobReset", playerSide);
                }
                break;
        }

    }

    private void GoalScored()
    {
        // Do some reset fanciness
        // Display callout
        UpdateState(States.PLAYING, null);
    }

    private IEnumerator OobReset(string playerSide)
    {
        while(resetTimer > 0.0f)
        {
            Debug.Log(resetTimer);
            resetTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.2f);
        }
        Vector2 offset = Vector2.zero;
        if (playerSide == "PlayerFrog")
        {
            offset = new Vector2(0.0f, -6.0f);
        }
        else if (playerSide == "AIFrog")
        {
            offset = new Vector2(0.0f, 3.0f);
        }
        ResetPlayers();
        ResetBall(offset);
    }

    private IEnumerator ResetField(GameObject goalObject)
    {
        ParticleSystem particleSystem = goalObject.GetComponent<ParticleSystem>();
        while (particleSystem.IsAlive())
        {
            yield return new WaitForSeconds(0.2f);
        }
        ResetPlayers();
        ResetBall(Vector2.zero);
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        UpdateState(States.PLAYING, null);
    }

    private void ResetPlayers()
    {
        player1.GetComponent<PlayerController>().ResetPosition();
        PlayerController p2c = player2.GetComponent<PlayerController>();
        if (p2c != null)
        {
            player2.GetComponent<PlayerController>().ResetPosition();
        } else
        {
            player2.GetComponent<AIController>().ResetPosition();
        }
    }

    private void ResetBall(Vector2 offset)
    {
        ball.transform.position = centreCircle + offset;
    }
}
