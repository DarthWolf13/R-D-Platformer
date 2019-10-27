using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 20f;
    public float fallMod = 2.5f;
    public float hopMod = 2f;

    public bool Grounded = false;
    Rigidbody2D player;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && Grounded == true)
        {
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
