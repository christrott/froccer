using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Left,
    Right
}

public class Car : MonoBehaviour {
    public Direction direction;
    public CarSpawner spawner;

    private float moveSpeed;

	void Start() {
        moveSpeed = 2.0f;
	}
	
	void Update() {
		if (direction == Direction.Right)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        if (direction == Direction.Left)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boundary")
        {
            spawner.RemoveCar(gameObject);
        }
    }
}
