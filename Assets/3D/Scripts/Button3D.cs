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
        else if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box")){
            pressedThisFrame = true;
        }
    }

    public void OnActivate() {
        onActive?.Invoke();

    }

    public void OnDisactivate() {
        onDisactive?.Invoke();
    }

    private void FixedUpdate() {
        if (pressedThisFrame != pressed){
            if (pressedThisFrame){
                animator.SetTrigger("Press");
                Invoke("OnActivate", 0.2f);
            }

            else {
                animator.SetTrigger("Unpress");
                Invoke("OnDisactivate", 0.2f);
            }
        }
        
        pressed = pressedThisFrame;
        
        pressedThisFrame = false;
    }
}
