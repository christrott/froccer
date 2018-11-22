using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerRenderer : MonoBehaviour {
    GameState gameState;
    TextMeshProUGUI textMesh;
    GameObject overtimeMesh;

	void Start() {
        gameState = FindObjectOfType<GameState>();
        textMesh = GetComponent<TextMeshProUGUI>();
        overtimeMesh = transform.parent.Find("TimerOvertimeText").gameObject;
    }
	
	void OnGUI() {
        float timerValue = gameState.Timer;
        float minutes = Mathf.Floor(timerValue / 60.0f);
        float seconds = Mathf.Floor(timerValue - (minutes * 60));
        if (minutes < 0)
        {
            // Handle Overtime
            overtimeMesh.SetActive(true);
            seconds = 60 - Mathf.Floor(timerValue - (minutes * 60));
            minutes++;
        }
        string secondsText = (seconds < 10) ? '0' + seconds.ToString() : seconds.ToString();
        string timerText = Mathf.Abs(minutes).ToString() + ":" + secondsText;
        textMesh.SetText(timerText);
	}
}
