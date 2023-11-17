using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaz : Gun
{

    public static int maxAmmo = 10, curAmmo = 10;

    private void Start()
    {
        firstUpgradeBonus = 15f;
        upgradeMultiplier = 1.5f;
        fireSpeed = 0.5f;
        duration = 1.2f;
        startFunctions();
        curUpgradeLevel = GameManager.Instance.gasLevel;
        calculateDamage();
        calculateDuration();
        calculateFireSpeed();

        Destroy(gameObject, duration);
    }

    public static void fillAmmo()
    {
        curAmmo = maxAmmo;
    }


}
