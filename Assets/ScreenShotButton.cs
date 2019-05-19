using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShotButton : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ScreenCapture.CaptureScreenshot("D:/", 2);
            Debug.Log("Captured!");
        }
    }
}