using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ActionObject
{
    public MovingPlatform leftDoor;
    public MovingPlatform rightDoor;
    public bool passingThroughHappened;
    public bool hasPassingThroughEvent;
    public bool willCloseWhenPassThrough;

    private void Awake()
    {
        passingThroughHappened = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && hasPassingThroughEvent && !passingThroughHappened)
        {
            OnActivate();
            if (willCloseWhenPassThrough)
                CloseThisDoor();
        }
    }

    public void OnActivate()
    {
       
    }

    private void CloseThisDoor()
    {
        leftDoor.OnDisactiveMovingPlatform();
        rightDoor.OnDisactiveMovingPlatform();
    }
}
