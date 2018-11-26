using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum States
{
    PLAYING,
    GOAL,
    OOB,
    GAMEOVER
}

public enum PlayStates
{
    NORMAL,
    RUSH,
    OVERTIME
}

public class GameState : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public GameObject ball;
    public States gameState;
    public PlayStates playState;
    public GameObject gameOverMenu;

    public Vector2 centreCircle;


    private float _timer;
    private int _team1Score;
    private int _team2Score;
    private float resetTimer;

    public float Timer
    {
        get {
            return _timer;
        }
    }

    public int Team1Score
    {
        get
        {
            return _team1Score;
        }
    }

    public int Team2Score
    {
        get
        {
            return _team2Score;
        }
    }



	// Use this for initialization
	void Start()
    {
        _timer = 300.0f;
        gameState = States.PLAYING;
        playState = PlayStates.NORMAL;
        centreCircle = ball.transform.position;
    }

    void Update()
    {
        if (gameState == States.PLAYING)
        {
            if (_timer < 5.0f && _timer > 0.0f)
            {
                _timer -= Time.deltaTime * 0.5f;
            }
            else
            {
                _timer -= Time.deltaTime;
            }
        }

        if (_timer < 0.0f && gameState == States.PLAYING)
        {
            if (_team1Score == _team2Score)
            {
                playState = PlayStates.OVERTIME;
            }
            else
            {
                UpdateState(States.GAMEOVER, null);
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
                    PlayerType playerType = stateSubject.GetComponent<PlayerController>().type;
                    resetTimer = 0.25f;
                    StartCoroutine("OobReset", playerType);
                }
                break;
            case States.GAMEOVER:
                // Do some stuff
                // Particle awesomeness
                // Callout awesomeness
                gameOverMenu.SetActive(true);
                TextMeshProUGUI menuCallout = gameOverMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                if (_team1Score > _team2Score)
                {
                    menuCallout.text = "Blue Team Won!";
                } else
                {
                    menuCallout.text = "Red Team Won!";
                }
                break;
        }

    }

    private void GoalScored()
    {
        // Do some reset fanciness
        // Display callout
    }

    private IEnumerator OobReset(PlayerType playerType)
    {
        
        while(resetTimer > 0.0f)
        {
            resetTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.2f);
        }
        Vector2 offset = Vector2.zero;
        if (playerType == PlayerType.Player1)
        {
            offset = new Vector2(0.0f, -4.0f);
        }
        else if (playerType == PlayerType.Player2 || playerType == PlayerType.Bot)
        {
            offset = new Vector2(0.0f, 4.0f);
        }
        ResetPlayers();
        ResetBall(offset);
        UpdateState(States.PLAYING, null);
    }

    private IEnumerator ResetField(GameObject goalObject)
    {
        ParticleSystem particleSystem = goalObject.GetComponent<ParticleSystem>();
        AddScore(goalObject.GetComponent<Goals>().goalTeam);
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
        ball.transform.rotation = Quaternion.identity;
        ball.GetComponent<Rigidbody2D>().rotation = 0.0f;
    }

    private void AddScore(Team teamScoredAgainst)
    {
        if (teamScoredAgainst == Team.Team1)
        {
            _team2Score++;
        }
        else if (teamScoredAgainst == Team.Team2)
        {
            _team1Score++;
        }
    }
}
