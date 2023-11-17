using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }

        GameManager.Instance.gameOver = true;
        GameObject.Find("Main Canvas").GetComponent<GameUIManager>().openGameOverPanel();
    }
}
