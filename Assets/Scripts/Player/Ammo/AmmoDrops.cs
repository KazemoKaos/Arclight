using System;
using UnityEngine;

public class AmmoDrops : MonoBehaviour
{
    [SerializeField] AmmoInventory ammoHeld;

    [SerializeField] GameObject primaryAmmoPrefab;
    [SerializeField] GameObject secondaryAmmoPrefab;
    [SerializeField] GameObject heavyAmmoPrefab;

    public static Action<GameObject> DropAmmoEvent;

    public float priPercent;
    public float secPercent;
    public float hevPercent;

    // If primary <40% then increase ammo drop chance
        // If above 75% don't drop
    // If secondary <35% then increase ammo drop chance
        // If above 60% don't drop
    // If heavy <15% then increase ammo drop chance
        // If above 50% don't drop

    public void DropAmmo(GameObject enemy)
    {
        int priAmmo = ammoHeld.GetAmmoAmount(AmmoTypes.PRIMARY);
        int secAmmo = ammoHeld.GetAmmoAmount(AmmoTypes.SECONDARY);
        int hevAmmo = ammoHeld.GetAmmoAmount(AmmoTypes.HEAVY);

        int maxPri = ammoHeld.GetMaxAmmoAmount(AmmoTypes.PRIMARY);
        int maxSec = ammoHeld.GetMaxAmmoAmount(AmmoTypes.SECONDARY);
        int maxHev = ammoHeld.GetMaxAmmoAmount(AmmoTypes.HEAVY);

        if(priAmmo < (maxPri * priPercent))
        {
            // Drop primary
            Instantiate(primaryAmmoPrefab, enemy.transform.position, primaryAmmoPrefab.transform.rotation);
        }

        if(secAmmo < (maxSec * secPercent))
        {
            // Drop secondary
            Instantiate(secondaryAmmoPrefab, enemy.transform.position, secondaryAmmoPrefab.transform.rotation);
        }

        if(hevAmmo < (maxHev * hevPercent))
        {
            // Drop heavy
            Instantiate(heavyAmmoPrefab, enemy.transform.position, heavyAmmoPrefab.transform.rotation);
        }
    }

    private void OnEnable()
    {
        AbstractEnemy.EnemyDefeatDrop += DropAmmo;
    }

    private void OnDisable()
    {
        AbstractEnemy.EnemyDefeatDrop -= DropAmmo;
    }
}
