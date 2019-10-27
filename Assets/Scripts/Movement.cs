using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

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
        if (Input.GetButtonDown("Jump") && Grounded)
        {
            player.velocity = Vector2.up * jumpSpeed;
        }
    }
}
