using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen2DJoin : MonoBehaviour
{
    public Transform naturalJoinScreen;

    public Transform currentJoinScreen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ScreenJoin")
        {
            ScreenJoiner(other);
        }
    }

    private void ScreenJoiner(Collider other)
    {
        if (currentJoinScreen)
        {
            if (Vector3.Dot(transform.forward, other.transform.forward) > Vector3.Dot(transform.forward, currentJoinScreen.forward))
            {
                UpdateCurrentJoinScreen(other);
            }
        }
        else
        {
            UpdateCurrentJoinScreen(other);
        }
    }

    private void UpdateCurrentJoinScreen(Collider other)
    {
        Screen2DJoin otherScreen = other.GetComponent<Screen2DJoin>();
        otherScreen.currentJoinScreen = transform;
        currentJoinScreen = otherScreen.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ScreenJoin")
        {
            if (naturalJoinScreen){currentJoinScreen = naturalJoinScreen;}
            else {currentJoinScreen = null;}
        }
    }

}
