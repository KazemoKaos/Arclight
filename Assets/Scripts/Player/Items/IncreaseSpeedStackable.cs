using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedStackable : Item
{
    PlayerStats stats;
    public float speedIncrease;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
        stats.movementMult = stats.movementMult + (stats.movementMult * (speedIncrease*count));
        stats.UpdateMovementSpeed();
    }

    public override void ApplyEffect()
    {
        stats.movementMult = stats.movementMult + (stats.movementMult * (speedIncrease * count));
        stats.UpdateMovementSpeed();
    }
}
