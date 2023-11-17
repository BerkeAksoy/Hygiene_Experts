using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sabun : Gun
{

    private void Start()
    {
        firstUpgradeBonus = 8;
        upgradeMultiplier = 1.14f;
        fireSpeed = 0.5f;
        duration = 0.6f;
        startFunctions();
        curUpgradeLevel = GameManager.Instance.sabunLevel;
        calculateDamage();
        calculateDuration();
        calculateFireSpeed();

        Destroy(gameObject, duration);
    }


}
