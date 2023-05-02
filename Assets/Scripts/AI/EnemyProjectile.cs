using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float selfDestructTime;
    public float baseDamage;
    public string owner;

    [SerializeField] LayerMask ignoreLayers;

    private void Start()
    {
        Invoke(nameof(SelfDestruct), selfDestructTime);
    }

    void SelfDestruct() { Destroy(gameObject); }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Damagable") && (((1 << collision.gameObject.layer) & ignoreLayers) == 0))
        {
            if (collision.transform.GetComponent<IDamageable>() != null) { collision.transform.GetComponent<IDamageable>().Damage(baseDamage, owner); }
            else if (collision.transform.GetComponentInParent<IDamageable>() != null) { collision.transform.GetComponentInParent<IDamageable>().Damage(baseDamage, owner); }
            SelfDestruct();
        }
        else { SelfDestruct(); }
    }
}
