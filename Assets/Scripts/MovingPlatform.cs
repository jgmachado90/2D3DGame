using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
   
    [Tooltip("Transform used for target endPosition")]
    public Transform targetTransform;

    Vector3[] targetPosition;

    int targetID;

    Vector3 startTarget;

    [Tooltip("Duration to move from start position to target position")]
    public float duration;

    float startDuration;

    float timer = 0;

    [Tooltip("Timer that platform stops after reaching target")]
    public float waitTimer;

    Transform previousPlayerParent;

    [Tooltip("Object that will active this platform to move from start position to target Position (may be none)")]
    public ActionObject activeObject;

    bool looped;

    bool onWaitTimer;

    bool reachTarget = false;

    public enum MovingPlatformType { standard, moveWithAction, stopWithAction };

    [Tooltip("Use standard if plataform is always moving. Use moveWithAction if platform moves from A to B as result of a action. Use stopWithAction if plataform stops moving from A to B as result of a action")]
    public MovingPlatformType movingPlatformType;

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

        if (movingPlatformType != MovingPlatformType.moveWithAction) {
            looped = true;
        }
    }

    public void OnActiveMovingPlatform(){

        if (movingPlatformType == MovingPlatformType.moveWithAction) { 
            duration = startDuration;
            targetID = 0;
            looped = false;
            reachTarget = false;
            startTarget = targetPosition[1];
            if (timer > 0) {
                timer = startDuration - timer;
            }

        }
        else if (movingPlatformType == MovingPlatformType.stopWithAction){
            reachTarget = true;
        }
    }

    public void OnDisactiveMovingPlatform(){
        if (movingPlatformType == MovingPlatformType.moveWithAction) { 
            duration = startDuration;
            startTarget = targetPosition[0];
            if (timer > 0) {
                timer = startDuration - timer;
            }
            targetID = 1;
            looped = false;
            reachTarget = false;
        }
        else if (movingPlatformType == MovingPlatformType.stopWithAction) {
            reachTarget = false;
        }
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
        if (onWaitTimer) {
            timer += Time.deltaTime;
            if (timer >= waitTimer) {
                onWaitTimer = false;
                timer = 0;
            }
            return;
        }

        if (reachTarget) {
            return;
        }

        transform.position = Vector3.Lerp(startTarget, targetPosition[targetID], timer/duration);
        timer += Time.deltaTime;

        if (timer >= duration){
            startTarget = transform.position;
            timer = 0;
            if (looped){
                targetID = (targetID + 1) % targetPosition.Length;
                onWaitTimer = true;
            }
            else {
                reachTarget = true;
            }
        }
    }

}
