using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusC : Enemy
{
    void Start()
    {
        getElements();
        curHealth = maxHealth;
        _Speed = Random.Range(_Speed, _Speed + randomSpeedAdder);
        curSpeed = _Speed;
    }

    private void FixedUpdate()
    {
        calculateMovement();
    }
}
