using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamagable
{

    [SerializeField]
    protected float curHealth, maxHealth, curSpeed, _Speed, randomSpeedAdder;
    [SerializeField]
    protected int scoreValue, upgradeLevel;
    protected bool isHit, isAlive, speedChanged;
    protected GameObject myHB;
    protected Rigidbody2D myRB2D;
    protected CircleCollider2D myCC2D;
    [SerializeField]
    protected AudioClip deathSound;

    private void OnDestroy()
    {
        GameManager.Instance.enemyList.Remove(this);
    }

    protected virtual void getElements()
    {
        isAlive = true;
        speedChanged = false;
        myHB = transform.Find("Canvas/Health Bar").gameObject;
        myRB2D = GetComponent<Rigidbody2D>();
        myCC2D = GetComponent<CircleCollider2D>();

        if (myHB != null)
        {
            myHB.SetActive(false);
        }

        GameManager.Instance.enemyList.Add(this);
    }

    protected virtual void calculateMovement()
    {
        myRB2D.velocity = new Vector2(myRB2D.velocity.x, -curSpeed);
    }

    public virtual void upgrade()
    {
        upgradeLevel++;
        maxHealth += maxHealth / 3;
        _Speed += _Speed / 5;
        randomSpeedAdder += randomSpeedAdder / 4;
    }

    protected virtual void checkCollisions()
    {
        if (!isHit)
        {
            if (myCC2D.IsTouchingLayers(LayerMask.GetMask("Sabun")))
            {
                takeDamage(GameObject.Find("SabunGun(Clone)").GetComponent<Sabun>().getDamage());
            }

            if (myCC2D.IsTouchingLayers(LayerMask.GetMask("Kolonya")))
            {
                takeDamage(GameObject.Find("KolonyaGun(Clone)").GetComponent<Kolonya>().getDamage());
            }

            if (myCC2D.IsTouchingLayers(LayerMask.GetMask("Dezenfektan")))
            {
                takeDamage(GameObject.Find("DezenfektanGun(Clone)").GetComponent<Dezenfektan>().getDamage());
                if (!speedChanged)
                {
                    setSpeed(curSpeed / 2);
                }
            }

            if (myCC2D.IsTouchingLayers(LayerMask.GetMask("Gaz")))
            {
                takeDamage(GameObject.Find("GasGun(Clone)").GetComponent<Gaz>().getDamage());
            }
        }
    }

    private void Update()
    {
        checkCollisions();
    }

    public virtual void takeDamage(float value)
    {
        if (!isHit && curHealth > 0 && isAlive)
        {
            curHealth -= value;
            if (!myHB.activeInHierarchy && myHB != null)
            {
                myHB.SetActive(true);
            }

            myHB.GetComponent<HealthBar>().updateHealth(this);

            if (GameObject.Find("/" + name + "/Blood Particles"))
            {
                //StartCoroutine(bloodParticle());
            }

            if (curHealth > 0)
            {
                isHit = true;
                StartCoroutine(refreshIsHit());
            }
        }

        if (curHealth <= 0 && isAlive)
        {
            isAlive = false;
            StopAllCoroutines();
            curSpeed = 0;

            if (myHB != null)
            {
                myHB.SetActive(false);
            }

            if (GetComponent<CircleCollider2D>())
            {
                GetComponent<CircleCollider2D>().enabled = false;
            }

            GetComponent<Dropper>().dropCoin();

            GameManager.Instance.updateScore(scoreValue * GameObject.Find("Player").GetComponent<Player>().getScoreMultiplier());

            AudioSource.PlayClipAtPoint(deathSound, new Vector3(0, 0, 0));

            Destroy(gameObject);
        }
    }

    protected virtual IEnumerator refreshIsHit()
    {
        yield return new WaitForSeconds(0.35f);
        isHit = false;
    }

    protected virtual IEnumerator reSpeed()
    {
        yield return new WaitForSeconds(2f);
        setDefSpeed();
    }

    protected virtual void setSpeed(float value)
    {
        curSpeed = value;
        speedChanged = true;
        StartCoroutine(reSpeed());
    }

    protected virtual void setDefSpeed()
    {
        curSpeed = _Speed;
        speedChanged = false;
    }

    public virtual float getCurHealth()
    {
        return curHealth;
    }

    public virtual  float getMaxHealth()
    {
        return maxHealth;
    }
}
