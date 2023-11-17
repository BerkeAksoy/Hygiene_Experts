using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HygienePack : MonoBehaviour
{
    [SerializeField]
    private AudioClip bonusSound = null;
    private Vector2 toGo;

    private void Start()
    {
        toGo = new Vector2(-400, 800);

        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            GetComponentInChildren<Text>().text = "Hijyen Paketi";
        }
        else
        {
            GetComponentInChildren<Text>().text = "Hygiene Pack";
        }
    }

    private void Update()
    {
        float step = 600 * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, toGo, step);

        if ((Vector2)transform.position == toGo)
        {
            GameObject.Find("Main Canvas").GetComponent<GameUIManager>().fillAllAmmos();

            AudioSource.PlayClipAtPoint(bonusSound, new Vector3(0, 0, -10));
            Destroy(gameObject);
        }
    }
}
