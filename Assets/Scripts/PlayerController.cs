using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    CharacterController characterController;
    float lastRotation = 0.0f;

	// Use this for initialization
	void Start () {
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotation = GetRotation(horizontal, vertical);

        if (horizontal != 0 || vertical != 0)
        {
            // transform.Translate(Vector2.up * Time.deltaTime);
            Vector3 movement = new Vector3(horizontal, vertical);
            characterController.Move(movement * Time.deltaTime);
        } else
        {
            rotation = lastRotation;
        }

        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        lastRotation = rotation;
    }

    private float GetRotation(float horizontal, float vertical)
    {
        float rotation = 0.0f;

        if (horizontal > 0)
        {
            rotation = (vertical > 0) ? 315.0f : (vertical < 0) ? 225.0f : 270.0f;
        }
        else if (horizontal < 0)
        {
            rotation = (vertical > 0) ? 45.0f : (vertical < 0) ? 135.0f : 90.0f;
        }

        if (vertical < 0)
        {
            if (horizontal == 0)
            {
                rotation = 180.0f;
            }
        }

        return rotation;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
    }
}
