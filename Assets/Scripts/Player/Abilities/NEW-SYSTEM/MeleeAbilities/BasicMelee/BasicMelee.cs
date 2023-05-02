using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : MeleeAbilityTemplate
{
    float meleeTimer;
    int localCharges;
    AudioSource audioSource;

    private void Start()
    {
        localCharges = charges;
        UpdateMeleeCooldownMax?.Invoke(currentCooldownTime);
        UpdateMeleeChargeUI(localCharges);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(localCharges < charges)
        {
            if (meleeTimer >= currentCooldownTime)
            {
                localCharges++;
                meleeTimer = 0f;
                UpdateMeleeCooldownUI?.Invoke(meleeTimer);
                UpdateMeleeChargeUI?.Invoke(localCharges);
            }
            else { meleeTimer += Time.deltaTime; UpdateMeleeCooldownUI?.Invoke(meleeTimer); }
        }
    }

    public override void MeleeAttack(Transform playerTransform) 
    {
        audioSource.PlayOneShot(soundEffect);

        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, range)) 
        {
            if (hit.transform.CompareTag("Damagable"))
            {
                if (hit.transform.GetComponent<IDamageable>() != null) { hit.transform.GetComponent<IDamageable>().Damage(damage); }
                else if (hit.transform.GetComponentInParent<IDamageable>() != null) { hit.transform.GetComponentInParent<IDamageable>().Damage(damage); }
            }
            else if (hit.transform.CompareTag("CritDamagable"))
            {
                if (hit.transform.GetComponent<IDamageable>() != null) { hit.transform.GetComponent<IDamageable>().Damage(damage); }
                else if (hit.transform.GetComponentInParent<IDamageable>() != null) { hit.transform.GetComponentInParent<IDamageable>().Damage(damage); }
            }
        }

        StartCoroutine(ResetAbility(1));
    }

    /// <summary>
    /// This does the melee action.
    /// </summary>
    /// <param name="anim">The animator component that will play the melee animation</param>
    public override void UseMelee(Animator anim)
    {
        if (localCharges != 0)  // If not on cooldown
        {
            useMelee = true;
            anim.Play("MeleeAbility", 0, 0);
            localCharges--;
            UpdateMeleeChargeUI?.Invoke(localCharges);
        }
    }
}
