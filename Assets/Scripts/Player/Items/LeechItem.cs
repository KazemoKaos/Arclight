using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeechItem : Item
{
    PlayerHealth playerHealth;

    public int healAmount;

    private void Awake()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    void Effect(GameObject hitObj)
    {
        if(hitObj.GetComponent<IDamageable>() != null) 
        { 
            if(hitObj.GetComponent<IDamageable>().GetHealth > 0) playerHealth.Heal(healAmount * count);
        }
        else if(hitObj.GetComponentInParent<IDamageable>() != null) 
        { 
            if (hitObj.GetComponentInParent<IDamageable>().GetHealth > 0) playerHealth.Heal(healAmount * count); 
        }
    }

    private void OnEnable()
    {
        Projectile.Hit += Effect;
        Projectile.CritHit += Effect;
    }

    private void OnDisable()
    {
        Projectile.Hit -= Effect;
        Projectile.CritHit -= Effect;
    }
}
