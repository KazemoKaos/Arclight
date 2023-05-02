using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// These are the adjustable values that the weapons will use. Each weapon will get a reference to this script and be able to use these values
/// in addition to their own
/// </summary>
public class WeaponStatManager : MonoBehaviour
{
    public float damageMod;
    public float reloadMod;
    public float rofMod;
    public Vector2 recoilMod;
    public int magMod;

    void DamageMod(float amt) { damageMod += amt; }
    void ReloadMod(float amt) { reloadMod = amt; }
    void ROFMod(float amt) { rofMod += amt; }
    void RecoilMod(float amt) { recoilMod += new Vector2(amt, amt); }
    void MagMod(int amt) { magMod += amt; }

    private void OnEnable()
    {
        PlayerStats.UpdateDamage += DamageMod;
        PlayerStats.UpdateReload += ReloadMod;
        PlayerStats.UpdateROF += ROFMod;
        PlayerStats.UpdateRecoil += RecoilMod;
        PlayerStats.UpdateMag += MagMod;
    }

    private void OnDisable()
    {
        PlayerStats.UpdateDamage -= DamageMod;
        PlayerStats.UpdateReload -= ReloadMod;
        PlayerStats.UpdateROF -= ROFMod;
        PlayerStats.UpdateRecoil -= RecoilMod;
        PlayerStats.UpdateMag -= MagMod;
    }
}
