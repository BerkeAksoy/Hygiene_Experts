using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    private SpawnerKing king;
    private bool gameOver;

    private void Start()
    {
        king = GetComponentInParent<SpawnerKing>();
    }

    private void Update()
    {
        gameOver = GameManager.Instance.isGameOver();
    }

    public IEnumerator spawnRandomly(bool itsPartyTime, int virusLuck)
    {
        while (!gameOver)
        {
            if (itsPartyTime)
            {
                yield return new WaitForSeconds(Random.Range(1f, 2.5f));
            }
            else
            {
                yield return new WaitForSeconds(Random.Range(3f, 8f));
            }

            if (virusLuck > Random.Range(0, 100))
            {
                Instantiate(king.virusPrefabs[Random.Range(0, 4)], transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(king.virusPrefabs[Random.Range(4, 7)], transform.position, Quaternion.identity);
            }
        }
    }
}
