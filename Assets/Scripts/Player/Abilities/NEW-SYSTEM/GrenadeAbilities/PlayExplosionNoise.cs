using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayExplosionNoise : MonoBehaviour
{
    public AudioClip soundClip;

    private void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(soundClip);
    }
}
