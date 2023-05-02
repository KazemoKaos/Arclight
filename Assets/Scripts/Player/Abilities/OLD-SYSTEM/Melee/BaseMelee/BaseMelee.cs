using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "SubAbilityMelee/BaseMelee")]
public class BaseMelee : AbilityMelee
{
    public bool localMeleeOnCooldown { get => meleeOnCooldown; set => meleeOnCooldown = value; }    // If the melee is on cooldown or not

    public override void MeleeAttack(Transform playerTransform)
    {
        if (Physics.Raycast(playerTransform.position, playerTransform.forward, out RaycastHit hit, meleeRange))
        {
            Debug.Log(hit.transform);
            if (hit.transform.CompareTag("Damagable")) { hit.transform.GetComponent<IDamageable>().Damage(meleeDamage); }
        }
    }

    /// <summary>
    /// This does the melee action.
    /// </summary>
    /// <param name="anim">The animator component that will play the melee animation</param>
    public override void UseMelee(Animator anim)
    {
        if (!localMeleeOnCooldown)  // If not on cooldown
        {
            anim.Play("MeleeAbility");
            localMeleeOnCooldown = true;
        }
    }
}
