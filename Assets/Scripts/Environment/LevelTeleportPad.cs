using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelTeleportPad : MonoBehaviour
{
    [SerializeField] GameObject TeleportPadObj;

    public void SetTeleporterActive()
    {
        TeleportPadObj.SetActive(true);
    }

    private void OnEnable()
    {
        AbstractEnemy.BossDefeated += SetTeleporterActive;
    }

    private void OnDisable()
    {
        AbstractEnemy.BossDefeated -= SetTeleporterActive;
    }
}
