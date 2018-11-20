﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour {
    public GameObject gameState;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            Debug.Log("Goal!");
            GetComponent<ParticleSystem>().Emit(30);
            gameState.GetComponent<GameState>().UpdateState(States.GOAL, gameObject);
        }
    }
}
