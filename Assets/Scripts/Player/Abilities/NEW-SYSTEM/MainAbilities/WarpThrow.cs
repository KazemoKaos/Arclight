using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpThrow : MainAbilityTemplate
{
    int localCharges;
    float abilityTimer;
    public int throwForce;

    AudioSource audioSource;

    private void Start()
    {
        localCharges = charges;
        UpdateMainAbilityChargeUI?.Invoke(localCharges);
        UpdateMainAbilityCooldownMax?.Invoke(currentCooldownTime);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (localCharges < charges)
        {
            if (abilityTimer >= currentCooldownTime)
            {
                localCharges++;
                abilityTimer = 0f;
                UpdateMainAbilityCooldownUI?.Invoke(abilityTimer);
                UpdateMainAbilityChargeUI?.Invoke(localCharges);
            }
            else { abilityTimer += Time.deltaTime; UpdateMainAbilityCooldownUI?.Invoke(abilityTimer); }
        }
    }

    public override void UseAbility(Animator anim)
    {
        if (localCharges != 0)
        {
            useMain = true;
            anim.Play("MainAbility", 0, 0);
            localCharges--;
            UpdateMainAbilityChargeUI?.Invoke(localCharges);
        }
    }

    public override void MainAttack(Transform attackPoint)
    {
        audioSource.PlayOneShot(soundEffect);

        GameObject temp = Instantiate(abilityObj);
        temp.transform.position = attackPoint.position;
        temp.transform.rotation = Quaternion.LookRotation(-attackPoint.forward);
        temp.GetComponent<WarpKnife>().SetValues(damage, attackPoint.root);
        temp.GetComponent<Rigidbody>().velocity = attackPoint.forward * throwForce;

        StartCoroutine(ResetAbility(1));
    }
}
