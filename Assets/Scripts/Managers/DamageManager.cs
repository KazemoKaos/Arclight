using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    [SerializeField] GameObject damageText;

    /// <summary>
    /// Spawns in the damage number at the position of the invoking object and the amount of damage that was done
    /// </summary>
    /// <param name="damagedObject"></param>
    /// <param name="damage"></param>
    void SpawnDamageNumber(Transform damagedObject, float damage)
    {
        Instantiate(damageText, transform).GetComponent<DamageText>().Initialize(damage, damagedObject);
    }

    private void OnEnable()
    {
        IDamageable.DamageNumber += SpawnDamageNumber;
    }

    private void OnDisable()
    {
        IDamageable.DamageNumber -= SpawnDamageNumber;
    }
}
