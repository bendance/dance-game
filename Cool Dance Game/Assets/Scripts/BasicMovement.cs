using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private bool hittingAWall;
    private string last_moved;

    // Update is called once per frame
    // Not good to do physics in update
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (hittingAWall && (last_moved == "down" || movement.y < 0))
        {
            Debug.Log("won't let you move down");
            movement.y = 0;
        }
        else
        {
            Debug.Log("okay now");
            hittingAWall = false;
        }

        Debug.Log(hittingAWall);
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

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.name == "Background")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            hittingAWall = true;
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
