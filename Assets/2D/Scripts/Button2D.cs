using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button2D : ActionObject
{

    Animator animator;

    bool pressed = false;
    public  bool pressedThisFrame = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("2DBox") || collision.gameObject.tag == "2DPlayer")
        {
            pressedThisFrame = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("2DBox") || collision.gameObject.tag == "2DPlayer")
        {
            pressedThisFrame = false;
        }
    }

    public void OnActivate()
    {
        onActive?.Invoke();

    }

    public void OnDisactivate()
    {
        onDisactive?.Invoke();
    }

    private void FixedUpdate()
    {
        if (pressedThisFrame != pressed){
            if (pressedThisFrame){
                Debug.Log("Press");
                animator.SetTrigger("Press");
                Invoke(nameof(OnActivate), 0.2f);
            }

            else{
                Debug.Log("UNPRESS");
                animator.SetTrigger("Unpress");
                Invoke(nameof(OnDisactivate), 0.2f);
            }
        }

        pressed = pressedThisFrame;

        //pressedThisFrame = false;
    }
}
