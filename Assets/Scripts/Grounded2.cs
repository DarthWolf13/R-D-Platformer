using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded2 : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        Player = gameObject.transform.parent.gameObject;
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<Move2>().Grounded = true;
        }
        else if (collision.collider.tag == "Wall")
        {
            Player.GetComponent<Move2>().Wall = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Player.GetComponent<Move2>().Grounded = false;
        }
        else if (collision.collider.tag == "Wall")
        {
            Player.GetComponent<Move2>().Wall = false;
        }
    }
}