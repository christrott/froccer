using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerRenderer : MonoBehaviour {
    GameState gameState;
    TextMeshProUGUI textMesh;

	void Start() {
        gameState = FindObjectOfType<GameState>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }
	
	void OnGUI() {
        float timerValue = gameState.Timer;
        float minutes = Mathf.Floor(timerValue / 60.0f);
        float seconds = Mathf.Floor(timerValue - (minutes * 60));
        string timerText = minutes.ToString() + ":" + seconds.ToString();
        textMesh.SetText(timerText);
	}
}
