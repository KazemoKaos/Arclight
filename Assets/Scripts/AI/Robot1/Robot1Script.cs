using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot1Script : MonoBehaviour
{
    public int HP = 100;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            //play Death Animation
        }
        else
        {
            //Play get hit annimation
        }

    }
}
