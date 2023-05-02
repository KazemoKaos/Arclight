using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxCamera : MonoBehaviour
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        cam.fieldOfView = Camera.main.fieldOfView;
    }
}
