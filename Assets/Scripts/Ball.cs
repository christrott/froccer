using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rigidbody.velocity.x != 0 || rigidbody.velocity.y != 0)
        {
            // Custom drag
            rigidbody.velocity *= 0.9f;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision);
    }
}
