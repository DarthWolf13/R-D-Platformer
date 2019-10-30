using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
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
