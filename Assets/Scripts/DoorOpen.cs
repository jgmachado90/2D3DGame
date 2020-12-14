using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public Vector3 leftDoorTarget;
    public Vector3 rightDoorTarget;

    public Vector3 leftDoorClosedPosition;
    public Vector3 rightDoorClosedPosition;


    public Transform leftDoorOpenedPosition;
    public Transform rightDoorOpenedPosition;

    public bool willMove;
    public float openSpeed;

    public void OpenDoor()
    {
        willMove = true;
        leftDoorTarget = leftDoorOpenedPosition.position;
        rightDoorTarget = rightDoorOpenedPosition.position;

    }

    public void CloseDoor()
    {
        willMove = true;
        leftDoorTarget = leftDoorClosedPosition;
        rightDoorTarget = rightDoorClosedPosition;

    }

    // Start is called before the first frame update
    void Start()
    {
        willMove = false;
        leftDoorClosedPosition = leftDoor.position;
        rightDoorClosedPosition = rightDoor.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CloseDoor();
        }

        if (willMove)
        {
            float step = openSpeed * Time.deltaTime;
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftDoorTarget, step);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightDoorTarget, step);
            if(Vector3.Distance(leftDoor.position, leftDoorTarget) < 0.001f && Vector3.Distance(rightDoor.position, rightDoorTarget) < 0.001f)
            {
                willMove = false;
            }
        }
    }
}
