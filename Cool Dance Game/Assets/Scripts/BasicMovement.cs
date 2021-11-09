using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    /**
    This function appropriately sets the player to face the direction of the last button pushed
    */
    private void setLastMoved()
    {
        // Last moved code for animator 1 = down, 2 = right, 3 = up, 4 = left
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

    // Update is called once per frame
    // Not good to do physics in update
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Set animator parameters
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);

        setLastMoved();
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }
}
