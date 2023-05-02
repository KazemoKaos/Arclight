using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePlayerStart : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;

    void Start()
    {
        DontDestroy.instance.gameObject.transform.position = spawnPoint.transform.position;
    }
}
