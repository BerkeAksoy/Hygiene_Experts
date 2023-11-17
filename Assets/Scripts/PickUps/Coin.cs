using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField]
    private AudioClip pickSound = null;
    private int coinValue;
    private GameUIManager gameUIManager;
    private Vector2 toGo;

    private void Start()
    {
        toGo = new Vector2(-400, 800);
        gameUIManager = GameObject.Find("Main Canvas").GetComponent<GameUIManager>();
        coinValue = Random.Range(20, 100);
    }

    private void Update()
    {
        float step = 400 * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, toGo, step);

        if((Vector2)transform.position == toGo)
        {
            GameManager.Instance.inGameCoins += coinValue;
            GameManager.Instance.heldCoins += coinValue;
            gameUIManager.updateGold();
            AudioSource.PlayClipAtPoint(pickSound, new Vector3(0, 0, 0));
            Destroy(gameObject);
        }
    }
}
