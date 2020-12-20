using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch3D : ActionObject {
    
    public enum SwitchType { onoff, disactivateOnTimer};

    public SwitchType type;

    public float timerToDisactivate;

    float timer;

    public Animator anim;

    bool active;

    bool onActivation;

    bool onRange;

    private void Start() {
        if (!anim) { 
            anim = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onRange = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            onRange = true;
        }
    }

    void ActivateSwitch() {
        if (!(type == SwitchType.disactivateOnTimer && active)) {
            onActivation = false;
        }

        if (active) {
            onActive?.Invoke();
        }
        else {
            onDisactive?.Invoke();
        }
    }

    void SetSwitch (bool ac) {
        active = ac;
        if (active) {
            anim.SetTrigger("Activate");
        }
        else {
            anim.SetTrigger("Disactivate");
        }
        onActivation = true;
        Invoke(nameof(ActivateSwitch), 1f);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E) && !onActivation && onRange) {
            SetSwitch(!active);
        }

        if (active && type == SwitchType.disactivateOnTimer) {
            timer += Time.deltaTime;
            if (timer >= timerToDisactivate) {
                timer = 0;
                SetSwitch(false);
            }
        }
    }
}
