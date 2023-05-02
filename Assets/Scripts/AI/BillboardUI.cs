using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
