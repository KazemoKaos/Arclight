using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public int maxShield;
    public int enemyValue;
    public int enemyBaseLevel;

    public float baseDamage;

    public GameObject projectile;
    public Sprite enemyImage;
}
