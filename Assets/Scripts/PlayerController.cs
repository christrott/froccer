using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Player1,
    Player2,
    Bot
}

public enum Team
{
    Team1,
    Team2
}

public class PlayerController : MonoBehaviour {
    public PlayerType type;
    public Team team;

    public float playerMoveSpeed = 2.0f;
    public float flipDuration = 0.5f;
    public float flipPower = 5.0f;
    public float flipMoveSpeed = 3.0f;

    private float flattenedCooldown = 0.6f;
    private float flattenedTimer = 0.0f;
    private bool flattened = false;
    
    private Vector2 startPos;
    private bool flipping = false;
    private float flipTimer = 0.0f;

    private CharacterController characterController;
    private float lastRotation = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private Animator animator;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (flattened)
        {
            flattenedTimer -= Time.deltaTime;
            if (flattenedTimer <= 0.0f)
            {
                flattened = false;
                animator.ResetTrigger("isFlattened");
            }
        }
        else if (type == PlayerType.Player1 || type == PlayerType.Player2)
        {
            PlayerMove();
        }
    }

    public void PlayerMove()
    {
        bool pressedShot = false;
        if (type == PlayerType.Player1)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            pressedShot = Input.GetButton("Fire1");
        } else if (type == PlayerType.Player2)
        {
            horizontal = Input.GetAxisRaw("Horizontal2");
            vertical = Input.GetAxisRaw("Vertical2");
            pressedShot = Input.GetButton("Fire1_2");
        }
        float rotation = GetRotation(horizontal, vertical);

        if (flipping)
        {
            EndShot();
        }
        else if (pressedShot)
        {
            TakeShot();
        }

        if (horizontal != 0 || vertical != 0)
        {
            if (flipping)
            {
                transform.Translate(Vector2.up * flipMoveSpeed * Time.deltaTime);
            }
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

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    private void TakeShot()
    {
        animator.SetTrigger("isFlipping");
        flipTimer = flipDuration;
        flipping = true;
    }

    private void EndShot()
    {
        flipTimer -= Time.deltaTime;
        if (flipTimer <= 0.0f)
        {
            animator.ResetTrigger("isFlipping");
            flipping = false;
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
        else if (collision.collider.tag == "Vehicle" && !flattened)
        {
            flattened = true;
            flattenedTimer = flattenedCooldown;
            animator.SetTrigger("isFlattened");
        }
    }
}
