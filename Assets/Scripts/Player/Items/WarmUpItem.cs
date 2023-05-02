using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUpItem : Item
{
    PlayerStats stats;
    int counter;
    public int critHitRequirement = 5;
    float timer = 5f;

    // How long should the item wait for the player to get the number of critical hits
    float internalTimer;
    [SerializeField] float timerLimit = 2f;

    bool effectRunning;

    private void Awake()
    {
        stats = GetComponentInParent<PlayerStats>();
    }

    private void Update()
    {
        if(counter > 0)
        {
            if (internalTimer >= timerLimit)
            {
                counter = 0;
                internalTimer = 0f;
            }
            else { internalTimer += Time.deltaTime; }
        }
    }

    void Effect(GameObject hitObj)
    {
        if (!effectRunning)
        {
            if (counter < critHitRequirement) { counter++; internalTimer = 0f; }
            if (counter == critHitRequirement) { StartCoroutine("ItemEffect"); }
        }
    }

    IEnumerator ItemEffect()
    {
        effectRunning = true;
        stats.reloadSpeedMult += count;
        stats.UpdateReloadSpeed();
        yield return new WaitForSeconds(timer);
        stats.reloadSpeedMult -= count;
        stats.UpdateReloadSpeed();
        counter = 0;
        effectRunning = false;
    }

    private void OnEnable()
    {
        Projectile.CritHit += Effect;
    }

    private void OnDisable()
    {
        Projectile.CritHit -= Effect;
    }
}
