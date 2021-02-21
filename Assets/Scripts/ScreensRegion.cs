using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensRegion : MonoBehaviour
{
    public List<Screen2D> screensInRegion = new List<Screen2D>();
    public Screen2D firstScreen;
    public Screen2D lastScreen;

    private void Awake()
    {
        lastScreen = firstScreen;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            lastScreen = GetLastScreen();
            Scene2DManager.instance.DisableCurrentScene();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Scene2DManager.instance.currentSceneActive = lastScreen;
            Scene2DManager.instance.EnableCurrentScene();
        }
    }


    private Screen2D GetLastScreen()
    {
        Screen2D last = null;
        foreach(Screen2D screen in screensInRegion)
        {
            if(screen == Scene2DManager.instance.currentSceneActive)
            {

                last = screen;
            }
        }
        return last;
    }
}
