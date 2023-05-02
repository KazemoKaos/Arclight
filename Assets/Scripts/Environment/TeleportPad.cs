using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeleportPad : MonoBehaviour
{
    [SerializeField] GameObject reciever;
    [SerializeField] GameObject bossPrefab;

    public static Action PlayBossMusic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.position = reciever.transform.position;
            PlayBossMusic.Invoke();
            if(bossPrefab) bossPrefab.SetActive(true);
        }
    }
}
