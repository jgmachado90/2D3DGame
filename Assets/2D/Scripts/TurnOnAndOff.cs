using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAndOff : MonoBehaviour
{
    public float offScreenScaleY = 0.001f;
    public float onScreenScaleY = 1f;
    public float currentScaleTarget;
    public float speed;
    public bool changingScale;
    public float t = 0;

    public Transform fakeCamera;


    public void TurnOn()
    {
        Debug.Log("turnOn" + gameObject);
        fakeCamera.gameObject.SetActive(true);
        changingScale = true;
        currentScaleTarget = onScreenScaleY;
        t = 0;
    }
    public void TurnOff()
    {
        fakeCamera.gameObject.SetActive(false);
        changingScale = true;
        currentScaleTarget = offScreenScaleY;
        t = 0;
    }

    private void FixedUpdate()
    {
        
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, currentScaleTarget,transform.localScale.z), t * speed);

        if(Mathf.Abs(offScreenScaleY - transform.localScale.y) < 0.001f && currentScaleTarget == offScreenScaleY)
        {
            gameObject.SetActive(false);
        }
        t += Time.deltaTime;
    }


}
