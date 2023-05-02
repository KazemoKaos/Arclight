using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStatsUI : MonoBehaviour
{
    public AbstractWeapon weapon;

    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI reload;
    public TextMeshProUGUI ads;
    public TextMeshProUGUI ready;
    public TextMeshProUGUI recoil;
    public TextMeshProUGUI rof;
    public TextMeshProUGUI mag;

    void Start() 
    {
        SetStats();
    }

    void SetStats()
    {
        weaponName.text = weapon.GetName();

        switch ((int)weapon.GetRarity())
        {
            case 0:
                weaponName.color = Color.white;
                break;
            case 1:
                weaponName.color = Color.blue;
                break;
            case 2:
                weaponName.color = Color.magenta;
                break;
            case 3:
                weaponName.color = Color.yellow;
                break;
        }

        damage.text = weapon.baseDamage.ToString() + " | " + weapon.critDamage.ToString();
        reload.text = ((int)weapon.reloadSpeed).ToString();
        ads.text = ((int)Mathf.Abs(weapon.adsSpeed - 100)).ToString();  // This will show the ads speed as greater numbers but this is NOT reflected internally!
        ready.text = ((int)weapon.readySpeed).ToString();
        recoil.text = weapon.recoilAmount.x.ToString() + " | " + weapon.recoilAmount.y.ToString();
        rof.text = weapon.rateOfFire.ToString();
        mag.text = weapon.magazineSize.ToString();
    }

    private void OnEnable()
    {
        SetStats();
    }
}
