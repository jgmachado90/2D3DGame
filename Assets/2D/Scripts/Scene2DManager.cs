using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2DManager : MonoBehaviour
{
    public static Scene2DManager instance;

    public Transform spawnPositionLeft;
    public Transform spawnPositionRight;
    public Transform player;

    public Player2DMovement playerController;
    public Screen2D currentSceneActive;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentSceneActive.myScreen3D.GetComponent<TurnOnAndOff>().TurnOn();

    }

    public void ChangeScene(int direction)
    {
        Debug.Log("change scene");
        if (direction > 0)
        {
            foreach (Transform child in currentSceneActive.transform)
            {
                if (child.name == "RightJoin")
                {
                    ExchangeScenes(child);
                }
            }
            player.transform.position = new Vector3(spawnPositionLeft.position.x, player.position.y, player.position.z);
        }
        if (direction < 0)
        {
            foreach (Transform child in currentSceneActive.transform)
            {
                if (child.name == "LeftJoin")
                {
                    ExchangeScenes(child);
                }
            }
            player.transform.position = new Vector3(spawnPositionRight.position.x, player.position.y, player.position.z);
        }
    }

    private void ExchangeScenes(Transform child)
    {
        Debug.Log("exchange scenes");
        Debug.Log("child.GetComponent<Screen2DJoin>().currentJoinScreen" + child.GetComponent<Screen2DJoin>().currentJoinScreen);
        if (child.GetComponent<Screen2DJoin>().currentJoinScreen != null)
        {
            DisableCurrentScene();
            currentSceneActive = child.GetComponentInChildren<Screen2DJoin>().currentJoinScreen.GetComponentInParent<Screen2D>();
            EnableCurrentScene();
  
        }
    }

    public void EnableCurrentScene()
    {
        currentSceneActive.myScreen2D.gameObject.SetActive(true);
        currentSceneActive.myScreen3D.gameObject.SetActive(true);
        currentSceneActive.myScreen3D.GetComponent<TurnOnAndOff>().TurnOn();
        playerController.gameObject.SetActive(true);
    }

    public void DisableCurrentScene()
    {
        currentSceneActive.myScreen2D.gameObject.SetActive(false);
        currentSceneActive.myScreen3D.GetComponent<TurnOnAndOff>().TurnOff();
        playerController.gameObject.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
