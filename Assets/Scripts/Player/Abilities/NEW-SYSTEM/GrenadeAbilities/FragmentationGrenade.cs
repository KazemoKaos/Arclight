using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentationGrenade : GrenadeAbilityTemplate
{
    int localCharges;
    float grenadeTimer;

    AudioSource audioSource;

    private void Start()
    {
        localCharges = charges;
        UpdateGrenadeCooldownMax?.Invoke(currentCooldownTime);
        UpdateGrenadeChargeUI(localCharges);

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (localCharges < charges)
        {
            if (grenadeTimer >= currentCooldownTime)
            {
                localCharges++;
                grenadeTimer = 0f;
                UpdateGrenadeCooldownUI?.Invoke(grenadeTimer);
                UpdateGrenadeChargeUI?.Invoke(localCharges);
            }
            else { grenadeTimer += Time.deltaTime; UpdateGrenadeCooldownUI?.Invoke(grenadeTimer); }
        }
    }

    public override void GrenadeAttack(Transform playerTransform)
    {
        audioSource.PlayOneShot(soundEffect);

        GameObject temp = Instantiate(grenadeObj);
        temp.GetComponent<ExplosiveGrenade>().SetValues(detonationTime, blastRadius, damage);
        temp.transform.position = playerTransform.position;
        temp.GetComponent<Rigidbody>().velocity += playerTransform.forward * throwSpeed;

        StartCoroutine(ResetAbility(1));
    }

    /// <summary>
    /// This does the grenade action.
    /// </summary>
    /// <param name="anim">The animator component to play the grenade throw animation</param>
    public override void UseGrenade(Animator anim)
    {
        if (localCharges != 0)
        {
            useGrenade = true;
            anim.Play("GrenadeAbility", 0, 0);
            localCharges--;
            UpdateGrenadeChargeUI?.Invoke(localCharges);
        }
    }
}
