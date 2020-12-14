using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfActivateDelayed : MonoBehaviour
{
    public GameObject gO;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActivateMe", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateMe()
    {
        gO.SetActive(true);
    }
}
