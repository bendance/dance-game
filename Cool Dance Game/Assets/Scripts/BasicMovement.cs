using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    // Not good to do physics in update
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        // Last moved code 1 = down, 2 = right, 3 = up, 4 = left
        if(movement != Vector2.zero)
        {
            if(Mathf.Abs(movement.y) < Mathf.Abs(movement.x))
            {
                //right
                if (movement.x > 0)
                {
                    animator.SetInteger("last_moved", 2);
                }
                //left
                else
                {
                    animator.SetInteger("last_moved", 4);
                }
            }
            else
            {
                //up
                if (movement.y > 0)
                {
                    animator.SetInteger("last_moved", 3);
                }
                //down
                else
                {
                    animator.SetInteger("last_moved", 1);
                }
            }   
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}