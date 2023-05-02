using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public float selfDestructTime;
    public float baseDamage;
    public float critDamage;
    public float range;

    [SerializeField] LayerMask ignoreLayers;
    Vector3 startPos;

    public static Action<GameObject> Hit;
    public static Action<GameObject> CritHit;

    private void Start()
    {
        Invoke(nameof(SelfDestruct), selfDestructTime);
        startPos = transform.position;
    }

    public void SetValues(float dmg, float critDmg, float range)
    {
        baseDamage = dmg;
        critDamage = critDmg;
        this.range = range;
    }

    private void Update()
    {
        if (Vector3.Distance(startPos, transform.position) > range) { SelfDestruct(); }
    }

    void SelfDestruct() { Destroy(gameObject); }

    private void OnCollisionEnter(Collision collision)
    {
        if(Vector3.Distance(startPos, transform.position) > range/2) { 
            baseDamage = Mathf.RoundToInt(baseDamage / 1.3f); 
            critDamage = Mathf.RoundToInt(critDamage / 1.3f); 
        }


        if (collision.transform.CompareTag("Damagable") && (((1<<collision.gameObject.layer) & ignoreLayers) == 0))
        {
            if (collision.transform.GetComponent<IDamageable>() != null) { collision.transform.GetComponent<IDamageable>().Damage(baseDamage); }
            else if (collision.transform.GetComponentInParent<IDamageable>() != null) { collision.transform.GetComponentInParent<IDamageable>().Damage(baseDamage); }
            Hit?.Invoke(collision.gameObject);
            SelfDestruct();
        }
        else if (collision.transform.CompareTag("CritDamagable") && (((1 << collision.gameObject.layer) & ignoreLayers) == 0))
        {
            if (collision.transform.GetComponent<IDamageable>() != null) { collision.transform.GetComponent<IDamageable>().Damage(critDamage); }
            else if (collision.transform.GetComponentInParent<IDamageable>() != null) { collision.transform.GetComponentInParent<IDamageable>().Damage(critDamage); }
            CritHit?.Invoke(collision.gameObject);
            SelfDestruct();
        }
        else { SelfDestruct(); }
    }
}
