using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public float moveSpeed;
    public float maxMoveSpeed = 7f;
    public float jumpSpeed = 40f;

    public float fallMod = 5f;
    public float hopMod = 2f;

    public float earlyJumpTime = 0.2f;
    private float landJumpTime;

    public bool Grounded = false;
    public float lateJumpTime = 0.05f;
    private float fallJumpTime;

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

        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Move()
    {
        bool left = Input.GetKey("a");
        bool right = Input.GetKey("d");
        
        if (left && Grounded)
        {
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
                moveSpeed += 0.2f;
            }
        }

        if (right && Grounded)
        {
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
                moveSpeed += 0.2f;
            }
        }

        if (!left && !right && Grounded)
        {
            moveSpeed -= 0.05f;
        }

        moveSpeed = Mathf.Clamp(moveSpeed, 0, 7);
        
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
