using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveGrenade : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    private float explodeTime;
    private float explosionRadius;
    private float damage;
    public AudioClip soundClip;

    void Start()
    {
        Invoke(nameof(Explode), explodeTime);
    }

    public void SetValues(float explodeTimer, float radius, float grenadeDamage)
    {
        explodeTime = explodeTimer;
        explosionRadius = radius;
        damage = grenadeDamage;
    }

    /// <summary>
    /// The explosion of the actual grenade.
    /// </summary>
    void Explode()
    {
        // Play particle effect of the grenade
        GameObject temp = Instantiate(explosionEffect);
        temp.transform.position = transform.position;
        temp.GetComponent<PlayExplosionNoise>().soundClip = soundClip;

        // Get all colliers within a radius of the grenade
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 0);
        foreach (Collider hit in hits)
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

        // Delete the gameobject (can later remove this once particle effect is added)
        // The particle effect can delete the gameobject when it's finished playing
        Destroy(gameObject);
    }
}
