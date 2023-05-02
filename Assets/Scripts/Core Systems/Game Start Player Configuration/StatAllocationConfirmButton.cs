using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatAllocationConfirmButton : MonoBehaviour
{
    /*Turns off the button when there are still points left over to allocate.
     *Turns on the button when all points are allocated.
    */
    Button but;
    [SerializeField]StatAllocation stats;
    void Start()
    {
        but = GetComponent<Button>();
    }

    void Update()
    {
        checkAllocation();
    }

    void checkAllocation()
    {
        if (stats.pointsLeft == 0)
        {
            but.interactable = true;
        }
        else
        {
            but.interactable = false;
        }
    }
}
