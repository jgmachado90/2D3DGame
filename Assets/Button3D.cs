using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button3D : ActionObject {
 
    Animator animator;

    bool pressed = false, pressedThisFrame = false;

    public bool is2D;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other) {
        if (is2D && (other.gameObject.CompareTag("Box") || other.gameObject.name == "2DPlayer")){
            pressedThisFrame = true;
        }
        else if (other.gameObject.CompareTag("PlayerFoot") || other.gameObject.CompareTag("Box")){
            pressedThisFrame = true;
        }
    }

    private void FixedUpdate() {
        if (pressedThisFrame != pressed){
            if (pressedThisFrame){
                animator.SetTrigger("Pressed");  
                onActive?.Invoke();  
            }

            else {
                onDisactive?.Invoke();
                animator.SetTrigger("Unpressed");
            }
        }
        
        pressed = pressedThisFrame;
        
        pressedThisFrame = false;
    }
}
