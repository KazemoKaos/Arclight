using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarpStrike", menuName = "SubclassAbilities/WarpStrike")]
public class WarpStrike : MainAbility
{
    public bool localAbilityOnCooldown { get => abilityOnCooldown; set => abilityOnCooldown = value; }

    public float throwForce;
    public float damage;

    public override void UseMain(Animator anim)
    {
        if (!abilityOnCooldown)
        {
            anim.Play("MainAbility");
            abilityOnCooldown = true;
        }
    }

    public override void MainAttack(Transform attackPoint)
    {
        GameObject temp = Instantiate(abilityGameObj);
        temp.transform.position = attackPoint.position;
        temp.transform.rotation = Quaternion.LookRotation(-attackPoint.forward);
        temp.GetComponent<WarpKnife>().SetValues(damage, attackPoint.root);
        temp.GetComponent<Rigidbody>().velocity = attackPoint.forward * throwForce;
    }
}
