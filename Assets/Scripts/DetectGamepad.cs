using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGamepad : MonoBehaviour
{
    // Hide controller canvas if gamepad is detected

    public GameObject controllerCanvas;

    void Start()
    {
        controllerCanvas = GameObject.Find("Controller Canvas");
    }

    private void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            controllerCanvas.SetActive(false);
        }
        else
        {
            controllerCanvas.SetActive(true);
        }
    }
}
