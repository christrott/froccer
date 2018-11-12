using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (rigid.velocity.x != 0 || rigid.velocity.y != 0)
        {
            // Custom drag
            rigid.velocity *= 0.9f;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision);
    }
}
