using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerMovement : MonoBehaviour
{
    //References--------------------------
    public CharacterController controller;    

    public float speed = 12f;
    public float jumpHeight = 3f;

    //GroundCheckReferences
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask, platformMask;
    
   //-------------------------------------

    Vector3 _velocity;
    bool _isGrounded, _isOnPlatform;

    //This will be removed soon
    public float gravity = -9.81f;

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlatformCheck();
        PlayerMove();

        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

    }

    private void FixedUpdate() {
        _velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime * 2);
        ApplyGravity();
    }

    void Jump() {
        if (_isGrounded) {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (_isOnPlatform) {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            Vector3 dir = GetComponentInParent<MovingPlatform>().MovedSinceLastFrame;
            _velocity -= dir * 50;
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    private void PlatformCheck() {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit raycastHit, 0.1f, platformMask)) {
            _isOnPlatform = true;
            if (raycastHit.transform != null) { 
                transform.SetParent(raycastHit.transform);
            }
        }
        else {
            if (_isOnPlatform) { 
                transform.SetParent(null);
                _isOnPlatform = false;
            }
        }

    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Fall();
    }

    void Fall() {
        if ((_isGrounded || _isOnPlatform) && _velocity.y < 0) {
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
