using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLookAtPlayer : MonoBehaviour
{
    int player;

    private void Awake()
    {
        player = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == player)
        {
            Vector3 target = new Vector3(other.transform.position.x, 
                this.transform.position.y, other.transform.position.z);
            transform.LookAt(target);
        }
    }
}
