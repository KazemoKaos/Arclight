using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InventoryInst
{
    public StackableItems stackItem { get; private set; }
    public int sizeOfStack { get; private set; }

    public InventoryInst(StackableItems sourceItem)
    {
        stackItem = sourceItem;
        AddToStack();
    }

    public void AddToStack()
    {
        sizeOfStack++;
    }

    public void RemoveFromStack()
    {
        sizeOfStack--;
    }
}
