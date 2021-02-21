using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCameraInitializer : MonoBehaviour
{
    public Transform fakeCamera;
    public float fakeCameraDistance;
    public Screen2D myScreen;



    private void Awake()
    {
        InitializeFakeCamera();
    }

    private void InitializeFakeCamera()
    {
        fakeCamera.position = myScreen.transform.position;
        fakeCamera.position += myScreen.transform.forward * fakeCameraDistance;
        fakeCamera.LookAt(myScreen.transform.position);
    }

   
}
