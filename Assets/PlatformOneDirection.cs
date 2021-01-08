using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOneDirection : MonoBehaviour
{
    public float speed;
    bool moving;
    public Transform target;
    private void Start()
    {
        moving = true;
    }

    private void Update()
    {
        if(moving)
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
            moving = false;
    }
}
