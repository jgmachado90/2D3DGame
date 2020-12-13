using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2DManager : MonoBehaviour
{
    public static Scene2DManager instance;

    public Transform spawnPositionLeft;
    public Transform spawnPositionRight;
    public Transform player;

    public Screen2D currentSceneActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
      
    }

    public void ChangeScene(int direction)
    {
      
        currentSceneActive.myScreen2D.gameObject.SetActive(false);
        currentSceneActive.myScreen3D.gameObject.SetActive(false);
        if (direction > 0)
        {
            foreach(Transform child in currentSceneActive.transform)
            {
                if(child.name == "RightJoin")
                {
                    currentSceneActive = child.GetComponentInChildren<Screen2DJoin>().currentJoinScreen.GetComponentInParent<Screen2D>();
                }
            }
        }
        if (direction < 0)
        {
            foreach (Transform child in currentSceneActive.transform)
            {
                if (child.name == "LeftJoin")
                {
                    currentSceneActive = child.GetComponentInChildren<Screen2DJoin>().currentJoinScreen.GetComponentInParent<Screen2D>();
                }
            }
        }

        currentSceneActive.myScreen2D.gameObject.SetActive(true);
        currentSceneActive.myScreen3D.gameObject.SetActive(true);


        if (direction > 0) {
            player.transform.position = new Vector3(spawnPositionLeft.position.x, player.position.y, player.position.z);
        }
        else
        {
            player.transform.position = new Vector3(spawnPositionRight.position.x, player.position.y, player.position.z);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
