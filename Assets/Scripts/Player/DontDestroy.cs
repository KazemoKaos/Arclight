using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;

    public bool keepObject;

    void Awake()
    {
        if (keepObject)
        {
            if(instance != null) { Destroy(gameObject); }
            else { instance = this; DontDestroyOnLoad(gameObject); }
        }
    }
}
