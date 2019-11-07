using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    public float moveSpeed;
    public float jumpSpeed = 20f;

    public float fallMod = 2.5f;
    public float hopMod = 2f;

    public float earlyJumpTime = 0.2f;
    public float landJumpTime;

    public bool Grounded = false;
    public float lateJumpTime = 0.05f;
    public float fallJumpTime;

    Rigidbody2D player;

    Vector3 movement;
    Vector3 leftMovement = new Vector3(-1f, 0f, 0f);
    Vector3 rightMovement = new Vector3(1f, 0f, 0f);

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();   
    }

    void Update()
    {
        fallJumpTime -= Time.deltaTime;
        if (Grounded)
        {
            fallJumpTime = lateJumpTime;
        }

        Move();
        Jump();
    }

    void Move()
    {
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        
        if (left)
        {
            //Debug.Log("left");
            if (moveSpeed == 0f)
            {
                movement = leftMovement;

            }
            else if (movement == rightMovement)
            {
                moveSpeed -= 0.05f;
            }
            

            if (movement == leftMovement)
            {
                moveSpeed += 0.5f;
            }
        }

        if (right)
        {
            //Debug.Log("right");
            if (moveSpeed == 0f)
            {
                movement = rightMovement;

            }
            else if (movement == leftMovement)
            {
                moveSpeed -= 0.05f;
            }
            

            if (movement == rightMovement)
            {
                moveSpeed += 0.5f;
            }
        }
        //if (left)
        //{
        //    movement = new Vector3(-1f, 0f, 0f);
        //    if (moveSpeed > 0)
        //    {

        //    }
        //}

        //if (right)
        //{
        //    movement = new Vector3(1f, 0f, 0f);
        //}

        //if (left || right)
        //{
        //    if (moveSpeed == 0)
        //    {
        //        moveSpeed += 0.5f;
        //    }
        //}

        if (!left && !right)
        {
            //Debug.Log("none");
            moveSpeed -= 0.05f;
        }

        moveSpeed = Mathf.Clamp(moveSpeed, 0, 5);
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Jump()
    {
        landJumpTime -= Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            landJumpTime = earlyJumpTime;
        }

        if (landJumpTime > 0 && fallJumpTime > 0)
        {
            landJumpTime = 0;
            fallJumpTime = 0;
            player.velocity = Vector2.up * jumpSpeed;
        }

        if (player.velocity.y < 0)
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (fallMod - 1) * Time.deltaTime;
        }
        else if (player.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (hopMod - 1) * Time.deltaTime;
        }
    }
}
