using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [Tooltip("Transform used for target endPosition")]
    public Transform targetTransform;

    Vector3[] targetPosition;

    [Tooltip("Duration to move from start position to target position")]
    public float duration;

    [Tooltip("Timer that platform stops after reaching target")]
    public float waitTimer;

    [Tooltip("Object that will active this platform to move from start position to target Position (may be none)")]
    public ActionObject activeObject;

    float moveLerpTimer = 0;

    int signal = 1;

    public enum PlatformState { moving, stopped, returning };

    PlatformState state;

    public enum MovingPlatformType { standard, moveWithAction, stopWithAction };

    [Tooltip("Use standard if plataform is always moving. Use moveWithAction if platform moves from A to B as result of a action. Use stopWithAction if plataform stops moving from A to B as result of a action")]
    public MovingPlatformType movingPlatformType;
    private Vector3 lastPosition;

    private void Start() {
        targetPosition = new Vector3[2];
        targetPosition[1] = transform.position;
        targetPosition[0] = targetTransform.position;

        if (activeObject) {
            activeObject.onActive += OnActiveMovingPlatform;
            activeObject.onDisactive += OnDisactiveMovingPlatform;
        }

        if (movingPlatformType == MovingPlatformType.moveWithAction) {
            state = PlatformState.stopped;
        }
    }

    public void OnActiveMovingPlatform() {

        if (movingPlatformType == MovingPlatformType.moveWithAction) {
            state = PlatformState.moving;
            signal = 1;
        }
        else if (movingPlatformType == MovingPlatformType.stopWithAction) {
            state = PlatformState.stopped;
        }
    }

    public void OnDisactiveMovingPlatform() {
        if (movingPlatformType == MovingPlatformType.moveWithAction) {

            state = PlatformState.returning;
            signal = -1;
        }
        else if (movingPlatformType == MovingPlatformType.stopWithAction) {
            state = PlatformState.moving;
        }

    }

    public Vector3 MovedSinceLastFrame {
        get {
            if (lastPosition == Vector3.zero) {
                return Vector3.zero;
            }
            return lastPosition - transform.position;
        }
        
    }
    
        
    private void FixedUpdate() {

        if (state == PlatformState.stopped) {
            return;
        }
        float t = Mathf.Sin(moveLerpTimer * duration * 0.5f - 1.57f) * waitTimer;

        t = Mathf.InverseLerp(-1, 1, t);

        t = Mathf.Clamp(t, 0, 1);

        Debug.Log("t: " + t);

        if (t == 1 && movingPlatformType == MovingPlatformType.moveWithAction && state == PlatformState.moving) {
            return;
        }

        if (t != 1 && t != 0 && state != PlatformState.returning) {
            state = PlatformState.moving;
        }
        if (t == 0 && state == PlatformState.returning) {
            state = PlatformState.stopped;
        }
        lastPosition = transform.position;
        transform.position = Vector3.Lerp(targetPosition[1], targetPosition[0], t);
        
        moveLerpTimer += signal * Time.deltaTime;
    }

}
