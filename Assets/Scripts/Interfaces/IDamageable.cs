using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    public float GetHealth { get; }
    public float GetShield { get; }
    public void Damage(float damage, string ownerName = "Player");
    public void Heal(float amount);

    /// <summary>
    /// Event to display the damage number of the object that just took damage
    /// </summary>
    public static Action<Transform, float> DamageNumber;
    public static Action Hit;
}
