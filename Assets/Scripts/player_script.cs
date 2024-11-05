using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float moveSpeed;
    public LayerMask mask;
    public float setBack;
    public GameObject enemyObject;

    void Update()
    {
        Vector2 movePosition = rigidBody.position;

        /* Movement Controls */
        if (Input.GetKey(KeyCode.W) == true)
        {
            movePosition += Vector2.up * moveSpeed;

        }
        if (Input.GetKey(KeyCode.A) == true)
        {
            movePosition += Vector2.left * moveSpeed;

        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            movePosition += Vector2.right * moveSpeed;

        }
        if (Input.GetKey(KeyCode.S) == true)
        {
            movePosition += Vector2.down * moveSpeed;

        }

        rigidBody.MovePosition(movePosition);
    }
}