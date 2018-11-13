using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerMoveSpeed = 2.0f;
    public float flipDuration = 0.5f;
    public float flipPower = 5.0f;

    private bool flipping = false;
    private float flipTimer = 0.0f;

    private CharacterController characterController;
    private float lastRotation = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private Animator animator;

    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        float rotation = GetRotation(horizontal, vertical);

        if (flipping)
        {
            EndShot();
        }
        else if (Input.GetButton("Fire1"))
        {
            TakeShot();
        }

        if (horizontal != 0 || vertical != 0)
        {
            transform.Translate(Vector2.up * playerMoveSpeed * Time.deltaTime);
            animator.SetTrigger("isMoving");
        }
        else
        {
            animator.ResetTrigger("isMoving");
            rotation = lastRotation;
        }

        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        lastRotation = rotation;
    }

    private void TakeShot()
    {
        animator.SetTrigger("isFlipping");
        flipTimer = flipDuration;
        flipping = true;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void EndShot()
    {
        flipTimer -= Time.deltaTime;
        if (flipTimer <= 0.0f)
        {
            animator.ResetTrigger("isFlipping");
            flipping = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball" && flipping)
        {
            if (flipping)
            {
                Vector2 hitDirection = new Vector2(horizontal, vertical);
                collision.collider.GetComponent<Rigidbody2D>().AddForce(hitDirection * flipPower);
            }
        }
    }
}
