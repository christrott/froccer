using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public float flipCooldown = 2.0f;

    public AudioMixer mixer;
    public AudioClip tapClip;
    public AudioClip hitClip;
    public AudioClip runOverClip;

    private Vector2 startPos;
    private float flattenedCooldown = 0.6f;
    private float flattenedTimer = 0.0f;
    private bool flattened = false;
    private bool flipping = false;
    private float flipTimer = 0.0f;
    private float flipCooldownTimer = 0.0f;

    private CharacterController characterController;
    private float lastRotation = 0.0f;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;

    private Animator animator;
    private AudioSource tapSource;
    private AudioSource hitSource;
    private AudioSource runOverSource;

    private void Awake()
    {
        AudioMixerGroup sfxGroup = mixer.FindMatchingGroups("Sfx")[0];
        tapSource = gameObject.AddComponent<AudioSource>();
        tapSource.clip = tapClip;
        tapSource.outputAudioMixerGroup = sfxGroup;
        hitSource = gameObject.AddComponent<AudioSource>();
        hitSource.clip = hitClip;
        hitSource.outputAudioMixerGroup = sfxGroup;
        runOverSource = gameObject.AddComponent<AudioSource>();
        runOverSource.clip = runOverClip;
        runOverSource.outputAudioMixerGroup = sfxGroup;
    }

    // Use this for initialization
    private void Start ()
    {
        startPos = transform.position;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (flipCooldownTimer > 0.0f)
        {
            flipCooldownTimer -= Time.deltaTime;
        }
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
        else if (type == PlayerType.Bot)
        {
            BotMove();
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

        if (flipping)
        {
            EndShot();
        }
        else if (pressedShot)
        {
            TakeShot();
        }

        UpdatePlayer();
    }

    private void UpdatePlayer()
    {
        float rotation = GetRotation(horizontal, vertical);

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
        flipCooldownTimer = flipCooldown;
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

    private void BotMove()
    {
        Ball ball = FindObjectOfType<Ball>();
        Vector3 ballPos = ball.transform.position;
        Vector3 pos = transform.position;
        Vector3 heading = ballPos - pos;
        float ballDistance = heading.magnitude;

        if (ballDistance < 1.0f && !flipping && flipCooldownTimer <= 0.0f)
        {
            Debug.Log("Take Shot");
            TakeShot();
        }
        else if (flipping)
        {
            EndShot();
        }

        heading = heading / ballDistance;
        horizontal = Mathf.Round(heading.x);
        vertical = Mathf.Round(heading.y);
        UpdatePlayer();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            if (flipping)
            {
                Vector2 hitDirection = new Vector2(horizontal, vertical);
                collision.collider.GetComponent<Rigidbody2D>().AddForce(hitDirection * flipPower);
                if (!hitSource.isPlaying)
                {
                    hitSource.Play();
                }
            } else
            {
                Vector2 hitDirection = new Vector2(horizontal, vertical);
                collision.collider.GetComponent<Rigidbody2D>().AddForce(hitDirection * 4.0f);
                if (!tapSource.isPlaying)
                {
                    tapSource.Play();
                }
            }
        }
        else if (collision.collider.tag == "Vehicle" && !flattened)
        {
            flattened = true;
            flattenedTimer = flattenedCooldown;
            animator.SetTrigger("isFlattened");
            if (!runOverSource.isPlaying)
            {
                runOverSource.Play();
            }
        }
    }
}
