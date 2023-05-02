using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOcclusion : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public MeshRenderer[] meshRenderers;

    private void Awake()
    {
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            smr.enabled = false;
        }

        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.enabled = false;
        }
    }

    public void TurnOnRoom()
    {
        foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
        {
            smr.enabled = true;
        }

        foreach (MeshRenderer mr in meshRenderers)
        {
            mr.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach(SkinnedMeshRenderer smr in skinnedMeshRenderers)
            {
                smr.enabled = true;
            }

            foreach(MeshRenderer mr in meshRenderers)
            {
                mr.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            foreach (SkinnedMeshRenderer smr in skinnedMeshRenderers)
            {
                smr.enabled = false;
            }

            foreach (MeshRenderer mr in meshRenderers)
            {
                mr.enabled = false;
            }
        }
    }
}
