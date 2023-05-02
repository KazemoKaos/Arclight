using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOcclusion : MonoBehaviour
{
    [SerializeField] SphereCollider occlusionCollider;
    [SerializeField] bool occlusionEnabled;

    private void Start()
    {
        if(occlusionEnabled) occlusionCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SkinnedMeshRenderer>() != null) { other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true; }
        else if (other.gameObject.GetComponent<MeshRenderer>() != null) { other.gameObject.GetComponent<MeshRenderer>().enabled = true; }
        else if(other.gameObject.GetComponentsInChildren<MeshRenderer>() != null) 
        {
            MeshRenderer[] meshes;
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            for(int i = 0; i < meshes.Length; i++) { meshes[i].enabled = true; }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SkinnedMeshRenderer>() != null) { other.gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false; }
        else if (other.gameObject.GetComponent<MeshRenderer>() != null) { other.gameObject.GetComponent<MeshRenderer>().enabled = false; }
        else if (other.gameObject.GetComponentsInChildren<MeshRenderer>() != null) 
        {
            MeshRenderer[] meshes;
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshes.Length; i++) { meshes[i].enabled = false; }
        }
    }
}
