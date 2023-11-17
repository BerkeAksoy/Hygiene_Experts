using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{

    public string gameId = "0000000";
    public string placementId = "Banner";
    public bool testMode = false, bR = false, hiden = false;

    void Start()
    {
        if (!GameManager.Instance.noAdsBought)
        {
            Advertisement.Initialize(gameId, testMode);
            Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
            StartCoroutine(ShowBannerWhenReady());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.noAdsBought && !bR)
        {
            Advertisement.Banner.Hide();
            bR = true;
        }
        //else
        //{
            //if (GameManager.Instance.inGame && !hiden)
            //{
                //Advertisement.Banner.Hide();
                //hiden = true;
            //}
            //else if(!GameManager.Instance.inGame && hiden)
            //{
                //Advertisement.Banner.Show();
                //hiden = false;
            //}
        //}
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
    }
}