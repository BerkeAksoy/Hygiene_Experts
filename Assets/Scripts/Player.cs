using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float scoreMultiplier, _ScoreMultiplier;
    private Gun selectedGun;
    private float fireSpeed, nextFire = 0;
    private bool inDoubleDamage;
    private GameUIManager gUIM;

    void Start()
    {
        gUIM = GameObject.Find("Main Canvas").GetComponent<GameUIManager>();
        _ScoreMultiplier = 1;
        scoreMultiplier = _ScoreMultiplier;
        selectedGun = GameManager.Instance.guns[0];
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void changeGunID(int value)
    {
        selectedGun = GameManager.Instance.guns[value];
        fireSpeed = selectedGun.getFireSpeed();
    }

    private void shoot(Vector2 pos)
    {
        if (selectedGun != null)
        {
            if (Time.time > nextFire)
            {
                if (selectedGun.name.Equals("KolonyaGun"))
                {
                    if(Kolonya.curAmmo > 0)
                    {
                        Instantiate(selectedGun, pos, Quaternion.identity);
                        Kolonya.curAmmo--;
                        gUIM.updateAmmo("Kolonya");
                    }
                }
                else if (selectedGun.name.Equals("DezenfektanGun"))
                {
                    if (Dezenfektan.curAmmo > 0)
                    {
                        Instantiate(selectedGun, pos, Quaternion.identity);
                        Dezenfektan.curAmmo--;
                        gUIM.updateAmmo("Dezenfektan");
                    }
                }
                else if (selectedGun.name.Equals("GasGun"))
                {
                    if (Gaz.curAmmo > 0)
                    {
                        Instantiate(selectedGun, pos, Quaternion.identity);
                        Gaz.curAmmo--;
                        gUIM.updateAmmo("Gaz Dezenfektan");
                    }
                }
                else
                {
                    Instantiate(selectedGun, pos, Quaternion.identity);
                }

                nextFire = Time.time + fireSpeed;
            }
        }
        else
        {
            Debug.Log("selected gun is null");
        }
    }

    public bool isInDoubleDamage()
    {
        return inDoubleDamage;
    }

    public void setInDoubleDamage(bool value)
    {
        StopCoroutine(reDoubleDamage());
        inDoubleDamage = value;
        StartCoroutine(reDoubleDamage());
    }

    IEnumerator reDoubleDamage()
    {
        yield return new WaitForSeconds(8f);
        inDoubleDamage = false;
    }

    public float getScoreMultiplier()
    {
        return scoreMultiplier;
    }


    public void setScoreMultiplier(float value)
    {
        StopCoroutine(reDoubleScore());
        scoreMultiplier = value;
        StartCoroutine(reDoubleScore());
    }

    IEnumerator reDoubleScore()
    {
        yield return new WaitForSeconds(6f);
        scoreMultiplier = _ScoreMultiplier;
    }


    public void setFireSpeed(float value)
    {
        fireSpeed = value;
    }


}
