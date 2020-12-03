using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCrazy : MonoBehaviour
{
    // Start is called before the first frame update
    public float xSpeed, ySpeed, zSpeed;

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, zSpeed * Time.deltaTime);
    }
}
