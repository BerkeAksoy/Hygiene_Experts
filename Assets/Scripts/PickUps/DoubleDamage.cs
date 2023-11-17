using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleDamage : MonoBehaviour
{
    [SerializeField]
    private AudioClip bonusSound = null;
    private Vector2 toGo;

    private void Start()
    {
        toGo = new Vector2(-400, 800);

        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            GetComponentInChildren<Text>().text = "İki Katı Hijyen";
        }
        else
        {
            GetComponentInChildren<Text>().text = "Double Hygiene";
        }
    }

    private void Update()
    {
        float step = 500 * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, toGo, step);

        if ((Vector2)transform.position == toGo)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            player.setInDoubleDamage(true);

            AudioSource.PlayClipAtPoint(bonusSound, new Vector3(0, 0, -10));
            Destroy(gameObject);
        }
    }
}
