using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int itemID;
    public int count = 1;

    public virtual void ApplyEffect() { }
}
