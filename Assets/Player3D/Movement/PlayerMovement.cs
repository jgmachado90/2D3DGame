using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //References--------------------------
    public CharacterController controller;    

    public float speed = 12f;
    public float jumpHeight = 3f;

    //GroundCheckReferences
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    
   //-------------------------------------

    Vector3 _velocity;
    bool _isGrounded;

    //This will be removed soon
    public float gravity = -9.81f;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlayerMove();

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }
}
