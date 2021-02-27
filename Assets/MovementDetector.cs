using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    public Vector3 lastPosition;
    public Quaternion lastRotation;

    public Vector3 RotateAmount;

    public InteractionGenerator2D3D[] fakeCamerasList;


    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        //fakeCamerasList = FindObjectsOfType<InteractionGenerator2D3D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);

        if (lastPosition != transform.position || lastRotation != transform.rotation)
        {
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            SendMessageToUpdateFakeCameraInteractionGenerators();
        }

    }

    private void SendMessageToUpdateFakeCameraInteractionGenerators()
    {
        foreach (InteractionGenerator2D3D fakeCam in fakeCamerasList)
        {
            if(fakeCam.gameObject.activeSelf)
                fakeCam.UpdatePolygonCollider2D();
        }
    }
}