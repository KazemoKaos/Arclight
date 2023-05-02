using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grenades", menuName = "SubAbilityGrenade/Grenadea")]
public class Grenades : AbilityGrenade
{
    public bool localGrenadeOnCooldown { get => grenadeOnCooldown; set => grenadeOnCooldown = value; }      // If the grenade is on cooldown or not

    public override void GrenadeAttack(Transform playerTransform)
    {
        GameObject temp = Instantiate(grenadeObj);
        temp.GetComponent<ExplosiveGrenade>().SetValues(detonationTime, blastRadius, grenadeDamage);
        temp.transform.position = playerTransform.position;
        temp.GetComponent<Rigidbody>().AddForce(playerTransform.forward * throwSpeed, ForceMode.Impulse);
    }

    /// <summary>
    /// This does the grenade action.
    /// </summary>
    /// <param name="anim">The animator component to play the grenade throw animation</param>
    public override void UseGrenade(Animator anim)
    {
        if (!grenadeOnCooldown)
        {
            anim.Play("GrenadeAbility");
            grenadeOnCooldown = true;
        }
    }
}
