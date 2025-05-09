using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;


    Vector2 movement;


    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        movement.y = UnityEngine.Input.GetAxisRaw("Vertical");

    }

    private void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

    }
}
