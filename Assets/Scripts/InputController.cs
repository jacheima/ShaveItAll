using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private CameraController camControl;
    
    private void Start()
    {
        camControl = FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            camControl.cameraDisable = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            camControl.cameraDisable = true;
        }

       
    }
}
