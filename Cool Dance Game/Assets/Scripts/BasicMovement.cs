using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;
    Collider2D playerCollider;

    private bool hittingAnObject;
    private string last_moved;

    void Start() 
    {
        playerCollider = GetComponent<Collider2D>();
    }

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

        // Last moved code for animator 1 = down, 2 = right, 3 = up, 4 = left
        if(movement != Vector2.zero)
        {
            if(Mathf.Abs(movement.y) < Mathf.Abs(movement.x))
            {
                //right
                if (movement.x > 0)
                {
                    last_moved = "right";
                    animator.SetInteger("last_moved", 2);
                }
                //left
                else
                {
                    last_moved = "left";
                    animator.SetInteger("last_moved", 4);
                }
            }
            else
            {
                //up
                if (movement.y > 0)
                {
                    last_moved = "up";
                    animator.SetInteger("last_moved", 3);
                }
                //down
                else
                {
                    last_moved = "down";
                    animator.SetInteger("last_moved", 1);
                }
            }   
        }
    }

    private string objectHitDirection(double x, double y)
    {
        if (hittingAnObject)
        {
            // hitting wall that's on bottom or top
            if (movement.y < 0)
            {
                return "below";
            }

            if (movement.y > 0)
            {
                return "above";
            }

            // hitting wall that's on left of right
            if (movement.x > 0)
            {
                return "right";
            }

            if (movement.x < 0)
            {
                return "left";
            }
        }

        return "no hit";
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        hittingAnObject = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        hittingAnObject = false;
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}
