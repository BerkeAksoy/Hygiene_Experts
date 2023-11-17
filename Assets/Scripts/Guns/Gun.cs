using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{

    protected const int maxUpgradeLevel = 11;
    protected float damage, duration, fireSpeed, upgradeMultiplier, firstUpgradeBonus;
    protected int curUpgradeLevel;
    [SerializeField]
    protected float[] levelBonus;
    private Player player;

    protected GameObject projectile;
    [SerializeField]
    protected AudioClip fireSound;

    protected virtual void startFunctions()
    {
        levelBonus = new float[maxUpgradeLevel];
        levelBonus[0] = firstUpgradeBonus;
        for(int i=1; i < maxUpgradeLevel; i++)
        {
            levelBonus[i] = levelBonus[i - 1] * upgradeMultiplier;
        }
        player = GameObject.Find("Player").GetComponent<Player>();

        if (fireSound != null)
        {
            AudioSource.PlayClipAtPoint(fireSound, new Vector3(0, 0, -10));
        }
    }

    public virtual void calculateDamage()
    { 
        damage += levelBonus[curUpgradeLevel];

        if (player.isInDoubleDamage())
        {
            damage *= 2;
        }
    }

    public virtual void calculateFireSpeed()
    {
        fireSpeed -= levelBonus[curUpgradeLevel] / 100;

        if(fireSpeed < 0.1f)
        {
            fireSpeed = 0.1f;
        }

        player.setFireSpeed(fireSpeed);
    }

    public virtual void calculateDuration()
    {
        duration += levelBonus[curUpgradeLevel] / 100;

        if(duration > 3.5f)
        {
            duration = 3.5f;
        }

        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            ParticleSystem ps;
            CircleCollider2D cC2D;
            ps = GetComponentInChildren<ParticleSystem>();
            cC2D = GetComponent<CircleCollider2D>();

            ps.Stop();
            var main = ps.main;
            main.duration = duration;
            main.startLifetime = duration;

            cC2D.radius += levelBonus[curUpgradeLevel] / 100;
            ps.Play();
        }
    }

    public float getFireSpeed()
    {
        return fireSpeed;
    }

    public float getNextFire()
    {
        return fireSpeed;
    }

    public float getDamage()
    {
        return damage;
    }

}
