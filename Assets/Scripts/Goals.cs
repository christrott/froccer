using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour {
    public Vector2 centreCircle;
    public GameObject gameState;

	// Use this for initialization
	void Start () {
        centreCircle = FindObjectOfType<Ball>().transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            Debug.Log("Goal!");
            GetComponent<ParticleSystem>().Emit(30);
            StartCoroutine("ResetField", collider);
        }
    }

    private IEnumerator ResetField(Collider2D collider)
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        while(particleSystem.IsAlive())
        {
            yield return new WaitForSeconds(0.2f);
        }
        collider.transform.position = centreCircle;
        collider.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameState.GetComponent<GameState>().UpdateState(States.GOAL);
    }
}
