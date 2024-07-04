using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private enum MovementState { idle, running, jumping, falling };

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float dirX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimationState(rb, anim, sprite);
    }


    private void UpdateAnimationState(Rigidbody2D rb, Animator anim, SpriteRenderer sprite)
    {
        MovementState state;

        // Obtiene la instancia de PlayerMovement
        PlayerMovement playerMovement = rb.GetComponent<PlayerMovement>();

        if (playerMovement.enabled == true)
        {
            float dirX = PlayerMovement.dirX; // Obtiene dirX de PlayerMovement

            if (dirX > 0f)
            {
                state = MovementState.running;
                sprite.flipX = false;
            }
            else if (dirX < 0f)
            {
                state = MovementState.running;
                sprite.flipX = true;
            }
            else
            {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = MovementState.falling;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            state = MovementState.idle;
        }

        anim.SetInteger("state", (int)state);
    }

}
