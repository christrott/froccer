using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerRenderer : MonoBehaviour {
    private GameState gameState;
    private TextMeshProUGUI textMesh;
    private GameObject overtimeMesh;
    private GameObject rushMesh;

	void Start() {
        gameState = FindObjectOfType<GameState>();
        textMesh = GetComponent<TextMeshProUGUI>();
        overtimeMesh = transform.parent.Find("TimerOvertimeText").gameObject;
        rushMesh = transform.parent.Find("TimerRushText").gameObject;
    }
	
	void OnGUI() {
        float timerValue = gameState.Timer;
        float minutes = Mathf.Floor(timerValue / 60.0f);
        float seconds = Mathf.Floor(timerValue - (minutes * 60));
        if (gameState.playState == PlayStates.OVERTIME)
        {
            // Handle Overtime
            overtimeMesh.SetActive(true);
            seconds = 60 - Mathf.Floor(timerValue - (minutes * 60));
            minutes++;
        }
        else if (gameState.playState == PlayStates.RUSH)
        {
            rushMesh.SetActive(true);
        } else if (rushMesh.activeInHierarchy)
        {
            rushMesh.SetActive(false);
        }
        string secondsText = (seconds < 10) ? '0' + seconds.ToString() : seconds.ToString();
        string timerText = Mathf.Abs(minutes).ToString() + ":" + secondsText;
        textMesh.SetText(timerText);
	}
}
