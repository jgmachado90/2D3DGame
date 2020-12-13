using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
   
    public Transform targetTransform;

    Vector3[] targetPosition;

    int targetID;

    Vector3 startTarget;

    public float duration;

    float startDuration;

    float timer = 0;

    public float waiTimer;

    Transform previousPlayerParent;

    public ActionObject activeObject;

    public bool looped;

    bool onWaitTimer;

    public bool notSetAsParent;

    private void Start() {
        startDuration = duration;
        targetPosition = new Vector3[2];
        targetPosition[1] = transform.position;
        targetPosition[0] = targetTransform.position;
        startTarget = transform.position;

        if (activeObject){
            activeObject.onActive += OnActiveMovingPlatform;
            activeObject.onDisactive += OnDisactiveMovingPlatform;
            targetID = 1;
        }  
    }

    public void OnActiveMovingPlatform(){
        duration = startDuration - timer;
        startTarget = transform.position;
        targetID = 0;
        looped = false;
        timer = 0;
    }

    public void OnDisactiveMovingPlatform(){
        duration = startDuration - timer;
        timer = 0;
        startTarget = transform.position;
        targetID = 1;
        looped = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerFoot")){
            Transform player = other.gameObject.transform;
            previousPlayerParent = player.parent;
            player.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PlayerFoot")){
            Transform player = other.gameObject.transform;
            player.SetParent(previousPlayerParent);
        }
    }

    private void Update() {

        transform.position = Vector3.Lerp(startTarget, targetPosition[targetID], timer/duration);
        timer += Time.deltaTime;

        if (timer >= duration){
            startTarget = transform.position;
            timer = 0;
            if (looped){
                targetID = (targetID + 1) % targetPosition.Length;
                //onWaitTimer = 
            }
        }
    }

}
