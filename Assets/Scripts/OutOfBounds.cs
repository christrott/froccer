using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {
    public GameObject playerSide;
    public GameObject gameState;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            gameState.GetComponent<GameState>().UpdateState(States.OOB, playerSide);
        }
    }
}
