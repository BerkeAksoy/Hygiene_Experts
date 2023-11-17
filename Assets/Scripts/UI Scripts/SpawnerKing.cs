using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerKing : MonoBehaviour
{

    public Enemy[] virusPrefabs = null;
    private Spawner[] spawners;
    private bool itsPartyTime;
    private int randomPartyTime;
    private int virusLuck;

    private void Start()
    {
        virusLuck = 15;
        randomPartyTime = Random.Range(30, 46);
        GameManager.Instance.startPartyTimer();
        GameManager.Instance.startLevelTimer();
        spawners = GetComponentsInChildren<Spawner>();

        foreach(Spawner spawner in spawners)
        {
            spawner.StartCoroutine(spawner.spawnRandomly(false, virusLuck));
        }
    }

    private void Update()
    {
        float partyElapsedTime = GameManager.Instance.getElapsedPartyTime();
        float levelElapsedTime = GameManager.Instance.getElapsedLevelTime();

        if (partyElapsedTime > randomPartyTime && !itsPartyTime)
        {
            itsPartyTime = true;
            GetComponentInParent<AudioSource>().Play();

            foreach (Spawner spawner in spawners)
            {
                spawner.StopAllCoroutines();
                spawner.StartCoroutine(spawner.spawnRandomly(true, virusLuck));
            }

            foreach (Enemy enemy in virusPrefabs)
            {
                enemy.upgrade();
            }

            virusLuck += 8;

            StartCoroutine(reParty());
        }
    }

    IEnumerator reParty()
    {
        yield return new WaitForSeconds(8f);
        itsPartyTime = false;
        randomPartyTime = Random.Range(28, 54);
        GetComponentInParent<AudioSource>().Stop();
        GameManager.Instance.startPartyTimer();
        foreach (Spawner spawner in spawners)
        {
            spawner.StopAllCoroutines();
            spawner.StartCoroutine(spawner.spawnRandomly(false, virusLuck));
        }
    }


}
