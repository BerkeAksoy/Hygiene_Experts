using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField]
    private GameObject coinPrefab = null, dDPrefab = null, dSPrefab = null, hPPrefab = null;

    public int dropChance, dropPUChance, dropHPackChance;

    public void dropCoin()
    {
        if (Random.Range(1, 101) <= dropChance)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        if(Random.Range(1, 101) <= dropPUChance)
        {
            if(Random.Range(0, 2) > 0)
            {
                Instantiate(dDPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(dSPrefab, transform.position, Quaternion.identity);
            }
        }

        if (Random.Range(1, 101) <= dropHPackChance)
        {
            
            Instantiate(hPPrefab, transform.position, Quaternion.identity);
        }
    }
}
