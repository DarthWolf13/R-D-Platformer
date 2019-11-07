using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    public float moveSpeed = 5f;
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

        Jump();

        if (Input.GetKeyDown("a"))
        {
            movement = new Vector3(-1f, 0f, 0f);
        }

        if (Input.GetKeyDown("d"))
        {
            movement = new Vector3(1f, 0f, 0f);
        }

        if (Input.GetKeyDown("a") || Input.GetKeyDown("d"))
        {
            moveSpeed = 5f;
        }

        if (!Input.GetKey("a") && !Input.GetKey("d"))
        {
            moveSpeed -= 0.2f;
            moveSpeed = Mathf.Clamp(moveSpeed, 0, 5);
            //Debug.Log("slow");
        }

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
