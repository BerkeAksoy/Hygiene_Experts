using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kolonya : Gun
{

    public static int maxAmmo = 10, curAmmo = 10;

    private void Start()
    {
        firstUpgradeBonus = 10f;
        upgradeMultiplier = 1.2f;
        fireSpeed = 0.4f;
        duration = 1.1f;
        startFunctions();
        curUpgradeLevel = GameManager.Instance.kolonyaLevel;
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
