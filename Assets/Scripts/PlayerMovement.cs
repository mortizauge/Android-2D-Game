using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    PlayerControls controls;

    [SerializeField] private LayerMask jumpableGround;
    public Vector2 MoveInput;

    public static float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    float doubleJump;

    private enum MovementState { idle, running, jumping, falling };
    private int jumpCount = 0;
    public bool isDoubleJumping = false;

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Enable();

        controls.Land.Move.performed += ctx =>
        {
            dirX = ctx.ReadValue<float>();
        };

        controls.Land.Jump.performed += ctx => Jump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        doubleJump = jumpForce * 0.75f;
    }

    void Jump()
    {
        if (IsGrounded())
        {
            jumpCount = 0;
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
        else
        {
            if (jumpCount <= 1)
            {
                isDoubleJumping = true;
                anim.Play("Player_falling");
                jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, doubleJump);
                jumpCount++;
            }
        }
    }


    // Windows Controls

    // Update is called once per frame
    //void Update()
    //{
    //    dirX = Input.GetAxisRaw("Horizontal");


    //    rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);


    //    if (Input.GetButtonDown("Jump") && IsGrounded())
    //    {
    //        jumpSoundEffect.Play();
    //        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    //        jumpCount++;
    //    }
    //    else if (Input.GetButtonDown("Jump")
    //        && !IsGrounded()
    //        && jumpCount < 1
    //        && (rb.velocity.y < -.1f))
    //    {
    //        isDoubleJumping = true;
    //        anim.Play("Player_falling");
    //        jumpSoundEffect.Play();
    //        rb.velocity = new Vector2(rb.velocity.x, doubleJump);
    //        jumpCount++;
    //    }

    //    if (IsGrounded())
    //    {
    //        isDoubleJumping = false;
    //        jumpCount = 0;
    //    }
    //}

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
