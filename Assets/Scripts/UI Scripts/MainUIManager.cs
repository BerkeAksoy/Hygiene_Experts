using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class MainUIManager : MonoBehaviour
{
    private GameObject mainPanel, upgradePanel, knowledgePanel;

    private Text highestScoreText, highestScore, pText, uText, aText, gText, sText, kText, dText, gDText, uPTitleText, uPCoinText, returnText;
    private Image infoImage;
    private int onImage;
    string gameId = "0000000";
    bool testMode = false, nABC = true;
    public AudioClip confirmed, notConfirmed, buttonPressed;

    public Sprite[] tSprites, eSprites;

    private void Start()
    {
        mainPanel = GameObject.Find("Canvas/Main Panel");
        upgradePanel = GameObject.Find("Canvas/Upgrade Panel");
        knowledgePanel = GameObject.Find("Canvas/Knowledge Panel");

        // Main Panel
        highestScoreText = GameObject.Find("Canvas/Main Panel/Highest Score Text").GetComponent<Text>();
        highestScore = GameObject.Find("Canvas/Main Panel/Highest Score").GetComponent<Text>();
        pText = GameObject.Find("Canvas/Main Panel/Buttons/Protection Button/Text").GetComponent<Text>();
        uText = GameObject.Find("Canvas/Main Panel/Buttons/Upgrade Button/Text").GetComponent<Text>();
        aText = GameObject.Find("Canvas/Main Panel/Buttons/No Ads Button/Text").GetComponent<Text>();
        gText = GameObject.Find("Canvas/Main Panel/Buttons/Play Button/Text").GetComponent<Text>();

        // Upgrade Panel
        uPCoinText = GameObject.Find("Canvas/Upgrade Panel/Coin/Text").GetComponent<Text>();
        uPTitleText = GameObject.Find("Canvas/Upgrade Panel/Title").GetComponent<Text>();
        sText = GameObject.Find("Canvas/Upgrade Panel/Buttons/Sabun Upgrade Button/Text").GetComponent<Text>();
        kText = GameObject.Find("Canvas/Upgrade Panel/Buttons/Kolonya Upgrade Button/Text").GetComponent<Text>();
        dText = GameObject.Find("Canvas/Upgrade Panel/Buttons/Dezenfektan Upgrade Button/Text").GetComponent<Text>();
        gDText = GameObject.Find("Canvas/Upgrade Panel/Buttons/Gaz Dezenfektan Upgrade Button/Text").GetComponent<Text>();

        // Knowledge Panel
        infoImage = GameObject.Find("Canvas/Knowledge Panel/Info Image").GetComponent<Image>();
        returnText = GameObject.Find("Canvas/Knowledge Panel/Return Button/Text").GetComponent<Text>();

        changeLanguage(GameManager.Instance.selectedLanguage);
        updateHighestScoreText();

        openMainPanel();
    }

    private void Update()
    {
        if (GameManager.Instance.noAdsBought && nABC)
        {
            GameObject.Find("Canvas/Main Panel/Buttons/No Ads Button").GetComponent<Button>().interactable = false;
            nABC = false;
        }
    }

    public void openMainPanel()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonPressed);

        closeHowToProtect();
        closeUpgrades();

        mainPanel.SetActive(true);
    }

    public void closeMainPanel()
    {
        mainPanel.SetActive(false);
    }

    public void openHowToProtect()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonPressed);

        closeMainPanel();

        knowledgePanel.SetActive(true);
        onImage = 0;

        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            infoImage.sprite = tSprites[onImage];
            returnText.text = "Ana Menüye\nDön";
        }
        else
        {
            infoImage.sprite = eSprites[onImage];
            returnText.text = "Return Main\nMenu";
        }

        if (!GameManager.Instance.noAdsBought)
        {
            Advertisement.Initialize(gameId, testMode);
            Advertisement.Show();
        }
    }

    public void nextImage()
    {
        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            if(onImage < tSprites.Length - 1)
            {
                onImage++;
            }
            else
            {
                onImage = 0;
            }

            infoImage.sprite = tSprites[onImage];
        }
        else
        {
            if (onImage < eSprites.Length - 1)
            {
                onImage++;
            }
            else
            {
                onImage = 0;
            }

            infoImage.sprite = eSprites[onImage];
        }

        GetComponent<AudioSource>().PlayOneShot(buttonPressed);
    }

    public void previousImage()
    {
        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            if (onImage > 0)
            {
                onImage--;
            }
            else
            {
                onImage = tSprites.Length - 1;
            }

            infoImage.sprite = tSprites[onImage];
        }
        else
        {
            if (onImage > 0)
            {
                onImage--;
            }
            else
            {
                onImage = tSprites.Length - 1;
            }

            infoImage.sprite = eSprites[onImage];
        }

        GetComponent<AudioSource>().PlayOneShot(buttonPressed);
    }

    public void closeHowToProtect()
    {
        knowledgePanel.SetActive(false);
    }

    public void openUpgrades()
    {
        mainPanel.SetActive(false);
        upgradePanel.SetActive(true);
        updateLevelImagesPrices();
        updateLock();
    }

    public void closeUpgrades()
    {
        if (upgradePanel.activeInHierarchy)
        {
            upgradePanel.SetActive(false);
        }
    }

    public void upgradeGun(string name)
    {
        GameObject sabunB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Sabun Upgrade Button");
        GameObject kolonyaB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Kolonya Upgrade Button");
        GameObject dezenfektanB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Dezenfektan Upgrade Button");
        GameObject gasB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Gaz Dezenfektan Upgrade Button");

        int i = -1;
        int price = 0;
        int coins = GameManager.Instance.heldCoins;

        switch (name)
        {
            case "Sabun":
                i = GameManager.Instance.sabunLevel;
                if (i < 10)
                {
                    if (i < 9)
                    {
                        price = sabunB.GetComponent<LevelCount>().prices[i];
                    }

                    if (coins >= price)
                    {
                        GameManager.Instance.heldCoins -= price;
                        GameManager.Instance.sabunLevel++;
                        GameManager.Instance.saveGame();
                        updateLock();
                        updateLevelImagesPrices();
                        GetComponent<AudioSource>().PlayOneShot(confirmed);
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                    }
                }
                else
                {
                    GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                }
                break;
            case "Kolonya":
                i = GameManager.Instance.kolonyaLevel;

                if (GameManager.Instance.kolonyaLocked)
                {
                    price = kolonyaB.GetComponent<LevelCount>().prices[0];

                    if (coins >= price)
                    {
                        GameManager.Instance.heldCoins -= price;
                        GameManager.Instance.kolonyaLocked = false;

                        GetComponent<AudioSource>().PlayOneShot(confirmed);
                        GameManager.Instance.saveGame();

                        updateLevelImagesPrices();
                        updateLock();
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                    }
                }
                else
                {
                    if(i < 10)
                    {
                        price = kolonyaB.GetComponent<LevelCount>().prices[i + 1];

                        if (coins >= price)
                        {
                            GameManager.Instance.heldCoins -= price;
                            GameManager.Instance.kolonyaLevel++;

                            GetComponent<AudioSource>().PlayOneShot(confirmed);
                            GameManager.Instance.saveGame();

                            updateLevelImagesPrices();
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                        }
                    }
                }
                break;
            case "Dezenfektan":
                i = GameManager.Instance.dezenfektanLevel;

                if (GameManager.Instance.dezenfektanLocked)
                {
                    price = dezenfektanB.GetComponent<LevelCount>().prices[0];

                    if (coins >= price)
                    {
                        GameManager.Instance.heldCoins -= price;
                        GameManager.Instance.dezenfektanLocked = false;

                        GetComponent<AudioSource>().PlayOneShot(confirmed);
                        GameManager.Instance.saveGame();

                        updateLevelImagesPrices();
                        updateLock();
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                    }
                }
                else
                {
                    if (i < 10)
                    {
                        price = dezenfektanB.GetComponent<LevelCount>().prices[i + 1];

                        if (coins >= price)
                        {
                            GameManager.Instance.heldCoins -= price;
                            GameManager.Instance.dezenfektanLevel++;

                            GetComponent<AudioSource>().PlayOneShot(confirmed);
                            GameManager.Instance.saveGame();

                            updateLevelImagesPrices();
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                        }
                    }
                }
                break;
            case "Gas":
                i = GameManager.Instance.gasLevel;

                if (GameManager.Instance.gasLocked)
                {
                    price = gasB.GetComponent<LevelCount>().prices[0];

                    if (coins >= price)
                    {
                        GameManager.Instance.heldCoins -= price;
                        GameManager.Instance.gasLocked = false;

                        GetComponent<AudioSource>().PlayOneShot(confirmed);
                        GameManager.Instance.saveGame();

                        updateLevelImagesPrices();
                        updateLock();
                    }
                    else
                    {
                        GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                    }
                }
                else
                {
                    if (i < 10)
                    {
                        price = gasB.GetComponent<LevelCount>().prices[i + 1];

                        if (coins >= price)
                        {
                            GameManager.Instance.heldCoins -= price;
                            GameManager.Instance.gasLevel++;

                            GetComponent<AudioSource>().PlayOneShot(confirmed);
                            GameManager.Instance.saveGame();

                            updateLevelImagesPrices();
                        }
                        else
                        {
                            GetComponent<AudioSource>().PlayOneShot(notConfirmed);
                        }
                    }
                }
                break;
        }
    }

    public void updateLock()
    {
        GameObject kolonyaB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Kolonya Upgrade Button");
        GameObject dezenfektanB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Dezenfektan Upgrade Button");
        GameObject gasB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Gaz Dezenfektan Upgrade Button");

        if (GameManager.Instance.kolonyaLocked)
        {
            kolonyaB.GetComponent<Image>().sprite = kolonyaB.GetComponent<SwitchButton>().locked;
        }
        else
        {
            kolonyaB.GetComponent<Image>().sprite = kolonyaB.GetComponent<SwitchButton>().notSelected;
        }

        if (GameManager.Instance.dezenfektanLocked)
        {
            dezenfektanB.GetComponent<Image>().sprite = dezenfektanB.GetComponent<SwitchButton>().locked;
        }
        else
        {
            dezenfektanB.GetComponent<Image>().sprite = dezenfektanB.GetComponent<SwitchButton>().notSelected;
        }

        if (GameManager.Instance.gasLocked)
        {
            gasB.GetComponent<Image>().sprite = gasB.GetComponent<SwitchButton>().locked;
        }
        else
        {
            gasB.GetComponent<Image>().sprite = gasB.GetComponent<SwitchButton>().notSelected;
        }
    }

    public void updateLevelImagesPrices()
    {
        GameObject sabunB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Sabun Upgrade Button");
        GameObject kolonyaB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Kolonya Upgrade Button");
        GameObject dezenfektanB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Dezenfektan Upgrade Button");
        GameObject gasB = GameObject.Find("Canvas/Upgrade Panel/Buttons/Gaz Dezenfektan Upgrade Button");

        LevelCount sabunLevelC = sabunB.GetComponent<LevelCount>();
        LevelCount kolonyaLevelC = kolonyaB.GetComponent<LevelCount>();
        LevelCount dezenfektanLevelC = dezenfektanB.GetComponent<LevelCount>();
        LevelCount gasLevelC = gasB.GetComponent<LevelCount>();

        Image sabunLevelI = sabunB.GetComponentsInChildren<Image>()[1];
        Image kolonyaLevelI = kolonyaB.GetComponentsInChildren<Image>()[1];
        Image dezenfektanLevelI = dezenfektanB.GetComponentsInChildren<Image>()[1];
        Image gasLevelI = gasB.GetComponentsInChildren<Image>()[1];

        int sL = GameManager.Instance.sabunLevel;
        int kL = GameManager.Instance.kolonyaLevel;
        int dL = GameManager.Instance.dezenfektanLevel;
        int gDL = GameManager.Instance.gasLevel;

        sabunLevelI.sprite = sabunLevelC.sprites[sL];
        kolonyaLevelI.sprite = kolonyaLevelC.sprites[kL];
        dezenfektanLevelI.sprite = dezenfektanLevelC.sprites[dL];
        gasLevelI.sprite = gasLevelC.sprites[gDL];

        if (sL < 10)
        {
            Text sabunPriceTag = sabunB.GetComponentsInChildren<Text>()[1];
            sabunPriceTag.text = sabunLevelC.prices[sL].ToString();
        }
        else
        {
            GameObject.Find("Canvas/Upgrade Panel/Buttons/Sabun Upgrade Button/Price").SetActive(false);
        }



        if (GameManager.Instance.kolonyaLocked)
        {
            Text kolonyaPriceTag = kolonyaB.GetComponentsInChildren<Text>()[1];
            kolonyaPriceTag.text = kolonyaLevelC.prices[0].ToString();
        }
        else if (kL < 10)
        {
            Text kolonyaPriceTag = kolonyaB.GetComponentsInChildren<Text>()[1];
            kolonyaPriceTag.text = kolonyaLevelC.prices[kL + 1].ToString();
        }
        else
        {
            GameObject.Find("Canvas/Upgrade Panel/Buttons/Kolonya Upgrade Button/Price").SetActive(false);
        }




        if (GameManager.Instance.dezenfektanLocked)
        {
            Text dezenfektanPriceTag = dezenfektanB.GetComponentsInChildren<Text>()[1];
            dezenfektanPriceTag.text = dezenfektanLevelC.prices[0].ToString();
        }
        else if (dL < 10)
        {
            Text dezenfektanPriceTag = dezenfektanB.GetComponentsInChildren<Text>()[1];
            dezenfektanPriceTag.text = dezenfektanLevelC.prices[dL + 1].ToString();
        }
        else
        {
            GameObject.Find("Canvas/Upgrade Panel/Buttons/Dezenfektan Upgrade Button/Price").SetActive(false);
        }




        if (GameManager.Instance.gasLocked)
        {
            Text gasPriceTag = gasB.GetComponentsInChildren<Text>()[1];
            gasPriceTag.text = gasLevelC.prices[0].ToString();
        }
        else if (gDL < 10)
        {
            Text gasPriceTag = gasB.GetComponentsInChildren<Text>()[1];
            gasPriceTag.text = gasLevelC.prices[gDL + 1].ToString();
        }
        else
        {
            GameObject.Find("Canvas/Upgrade Panel/Buttons/Gaz Dezenfektan Upgrade Button/Price").SetActive(false);
        }


        uPCoinText.text = GameManager.Instance.heldCoins.ToString();
    }

    public void updateHighestScoreText()
    {
        highestScore.text = GameManager.Instance.highestScore.ToString();
    }

    public void changeLanguage(string language)
    {
        GameManager.Instance.selectedLanguage = language;

        if (language.Equals("Turkish"))
        {
            uPTitleText.text = "Geliştir";
            sText.text = "Sabun";
            kText.text = "Kolonya";
            dText.text = "Dezenfektan";
            gDText.text = "Gaz Dezenfektan";

            highestScoreText.text = "En İyi\n Skor";
            pText.text = "Karakter\n Bilgileri";
            uText.text = "Savunmanı\n Geliştir";
            aText.text = "Reklamları\n Kapat";
            gText.text = "Oyna";
        }
        else
        {
            uPTitleText.text = "Upgrade";
            sText.text = "Soap";
            kText.text = "Cologne";
            dText.text = "Disinfectant";
            gDText.text = "Fumigant";

            highestScoreText.text = "Highest\n Score";
            pText.text = "Character Information";
            uText.text = "Upgrade\n Your Defence";
            aText.text = "No Ads";
            gText.text = "Play";
        }

        GameManager.Instance.saveGame();
    }

    public void playGame()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonPressed);
        LevelManager.Instance.openGame();
        GameManager.Instance.restartValues();
    }


}
