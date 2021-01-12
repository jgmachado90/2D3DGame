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
            var interacting = IsIntersecting(transform, other.transform);
            if (interacting)
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
        //otherScreen.currentJoinScreen = transform;
        currentJoinScreen = otherScreen.transform;
    }


    bool IsIntersecting(Transform self, Transform other)
    {
        Transform L1_start = self;
        Vector3 L1_end = self.forward * 90;
        Transform L2_start = other;
        Vector3 L2_end = other.forward * 90;

        bool isIntersecting = false;

        //3d -> 2d
        Vector2 p1 = new Vector2(L1_start.position.x, L1_start.position.z);
        Vector2 p2 = new Vector2(L1_end.x, L1_end.z);

        Vector2 p3 = new Vector2(L2_start.position.x, L2_start.position.z);
        Vector2 p4 = new Vector2(L2_end.x, L2_end.z);

        float denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);

        //Make sure the denominator is > 0, if so the lines are parallel
        if (denominator != 0)
        {
            float u_a = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            float u_b = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;

            //Is intersecting if u_a and u_b are between 0 and 1
            if (u_a >= 0 && u_a <= 1 && u_b >= 0 && u_b <= 1)
            {
                isIntersecting = true;
            }
        }

        return isIntersecting;
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
