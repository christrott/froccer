using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rigid;

	void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }
	
	void Update() {
        if (rigid.velocity.x != 0 || rigid.velocity.y != 0)
        {
            // Custom drag
            rigid.velocity *= 0.9f;
        }
	}
}
