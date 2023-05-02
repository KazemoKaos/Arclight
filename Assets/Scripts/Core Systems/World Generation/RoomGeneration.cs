using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Local states of each room.
/// </summary>
public class RoomGeneration : MonoBehaviour
{
    [Tooltip("0 = North, 1 = East, 2 = South, 3 = West")]
    [SerializeField] GameObject[] doors;    // 0 = North, 1 = East, 2 = South, 3 = West

    MeshRenderer[] meshes;
    SkinnedMeshRenderer[] skinnedMeshes;
    [SerializeField] bool renderMeshes;

    private void Start()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();

        if (!renderMeshes)
        {
            for (int i = 0; i < meshes.Length; i++) { meshes[i].enabled = false; }
            for (int j = 0; j < skinnedMeshes.Length; j++) { skinnedMeshes[j].enabled = false; }
        }
    }

    /// <summary>
    /// Turns off the doors in accordance to their order
    /// </summary>
    /// <param name="status"></param>
    public void UpdateDoor(bool[] status)
    {
        for(int i = 0; i < status.Length; i++)
        {
            doors[i].GetComponent<Animator>().SetBool("Opened", status[i]);
        }

        if (status[2] == false && status[3] == false) { GetComponent<RoomOcclusion>().TurnOnRoom(); }
    }
}
