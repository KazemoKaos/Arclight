using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAllocation : MonoBehaviour
{
    //Used to store stat allocation

    // Stat elements
    [SerializeField] int maxPoints = 30;
    public int pointsLeft = 0;
    public int HPStat = 10;
    public int shieldStat = 10;
    public int CDRStat = 10;

    void Update()
    {
        pointsLeft = maxPoints - (HPStat + shieldStat + CDRStat);
    }
    public void decreaseHP()
    {
        if (pointsLeft < maxPoints && HPStat > 0)
        {
            HPStat--;
            pointsLeft++;
        }
        else return;
    }
    public void decreaseShield()
    {
        if (pointsLeft < maxPoints && shieldStat > 0)
        {
            shieldStat--;
            pointsLeft++;
        }
        else return;
    }
    public void decreaseCDR()
    {
        if (pointsLeft < maxPoints && CDRStat > 0)
        {
            CDRStat--;
            pointsLeft++;
        }
        else return;
    }

    public void increaseHP()
    {
        if (pointsLeft > 0)
        {
            pointsLeft--;
            HPStat++;
        }
        else return;
    }

    public void increaseShield()
    {
        if (pointsLeft > 0)
        {
            pointsLeft--;
            shieldStat++;
        }
        else return;
    }
    public void increaseCDR()
    {
        if (pointsLeft > 0)
        {
            pointsLeft--;
            CDRStat++;
        }
        else return;
    }
}
