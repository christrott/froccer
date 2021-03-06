﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour {
    public GameObject gameState;
    public Team goalTeam;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            audioSource.Play();
            GetComponent<ParticleSystem>().Emit(30);
            gameState.GetComponent<GameState>().UpdateState(States.GOAL, gameObject);
        }
    }
}
