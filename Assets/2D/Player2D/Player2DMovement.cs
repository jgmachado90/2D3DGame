﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2DMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool facingRight = true;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;

    public bool isGrounded;
    public LayerMask ground;
    public float groundCheckLenght;
    public Transform groundCheckPoint;

  
    public float gravity = -17f;


    private void Update()
    {
        
        MovePlayer();

    }
    private void FixedUpdate()
    {
        ApplyGravity();
    }


    private void ApplyGravity()
    {
        rb.AddForce(transform.up * gravity, ForceMode2D.Force);
    }

    private void MovePlayer()
    {

        if (!isGrounded)
            MoveX(moveSpeed * 0.5f);
        
        else
            MoveX(moveSpeed);
    

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckLenght, ground);

        if (Input.GetKeyDown(KeyCode.I) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.I) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

      

    }

    private void MoveX(float moveSpeed)
    {
        
  
        rb.velocity = new Vector2(Input.GetAxisRaw("2DHorizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);

        if (Input.GetAxisRaw("2DHorizontal") > 0)
        {
            sprite.flipX = false;
        }
        else if (Input.GetAxisRaw("2DHorizontal") < 0)
        {
            sprite.flipX = true;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "rightSceneChanger")
        {
            Scene2DManager.instance.ChangeScene(1);
        }
        if (collision.tag == "leftSceneChanger")
        {
            Scene2DManager.instance.ChangeScene(-1);
        }
    }
    public void Jump(float bounceForce)
    {
     
    }

    public void Die()
    {
       
    }

    public void Respawn()
    {
     
    }

}
