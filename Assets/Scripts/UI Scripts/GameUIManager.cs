using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{

    private Player player;
    private GameObject sabunB, kolonyaB, dezenfektanB, gasB, backlight, gameOverPanel;
    private Text sText, kText, dText, gDText, goldText, scoreText, pAT, wAT, dT, rT, kA, dA, gDA;
    public string[] tStrings, eStrings;

    void Start()
    {
        gameOverPanel = GameObject.Find("Main Canvas/Game Over Panel");
        backlight = GameObject.Find("Main Canvas/Backlight");

        // Main Canvas
        sabunB = GameObject.Find("Main Canvas/Buttons/Sabun");
        kolonyaB = GameObject.Find("Main Canvas/Buttons/Kolonya");
        dezenfektanB = GameObject.Find("Main Canvas/Buttons/Dezenfektan");
        gasB = GameObject.Find("Main Canvas/Buttons/Gas");

        sText = sabunB.GetComponentsInChildren<Text>()[0];
        kText = kolonyaB.GetComponentsInChildren<Text>()[0];
        dText = dezenfektanB.GetComponentsInChildren<Text>()[0];
        gDText = gasB.GetComponentsInChildren<Text>()[0];

        kA = kolonyaB.GetComponentsInChildren<Text>()[1];
        dA = dezenfektanB.GetComponentsInChildren<Text>()[1];
        gDA = gasB.GetComponentsInChildren<Text>()[1];

        goldText = GameObject.Find("Main Canvas/Score Image/Gold Text").GetComponent<Text>();
        scoreText = GameObject.Find("Main Canvas/Score Image/Score Text").GetComponent<Text>();

        // Game Over Panel
        pAT = GameObject.Find("Main Canvas/Game Over Panel/Play Again Button").GetComponentInChildren<Text>();
        wAT = GameObject.Find("Main Canvas/Game Over Panel/Watch Ad Button").GetComponentInChildren<Text>();
        dT = GameObject.Find("Main Canvas/Game Over Panel/Donate Button").GetComponentInChildren<Text>();
        rT = GameObject.Find("Main Canvas/Game Over Panel/Return Button").GetComponentInChildren<Text>();

        player = GameObject.Find("Player").GetComponent<Player>();

        sabunB.GetComponent<SwitchButton>().selectedImage();
        checkLocked();

        updateScore();
        updateGold();

        updateTextLanguage();

        closeGameOverPanel();

        fillAllAmmos();
    }

    public void openGameOverPanel()
    {
        backlight.SetActive(true);
        gameOverPanel.SetActive(true);
        GameManager.Instance.saveGame();
        Time.timeScale = 0;

        Text infoText = GameObject.Find("Main Canvas/Game Over Panel/Info Text").GetComponent<Text>();

        if (GameManager.Instance.selectedLanguage.Equals("Turkish"))
        {
            int i = Random.Range(0, tStrings.Length);

            infoText.text = tStrings[i];
        }
        else
        {
            int i = Random.Range(0, eStrings.Length);

            infoText.text = eStrings[i];
        }

        GameObject.Find("Spawn Line").GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }

    public void closeGameOverPanel()
    {
        gameOverPanel.SetActive(false);
        backlight.SetActive(false);
    }

    public void updateTextLanguage()
    {
        string language = GameManager.Instance.selectedLanguage;

        if (language.Equals("Turkish"))
        {
            sText.text = "Sabun";
            kText.text = "Kolonya";
            dText.text = "Dezenfektan";
            gDText.text = "Gaz Dezenfektan";

            pAT.text = "Yeniden Oyna";
            wAT.text = "Oynamaya Devam Et";
            dT.text = "Gelişmemize Destek Ol!";
            rT.text = "Ana Menüye Dön";
        }
        else
        {
            sText.text = "Soap";
            kText.text = "Cologne";
            dText.text = "Disinfectant";
            gDText.text = "Fumigant";

            pAT.text = "Play Again";
            wAT.text = "Revive";
            dT.text = "Support Us!";
            rT.text = "Return Main Menu";
        }
    }

    public void selectGun(int value)
    {
        SwitchButton sabunButton = GameObject.Find("Main Canvas/Buttons/Sabun").GetComponent<SwitchButton>();
        SwitchButton kolonyaButton = GameObject.Find("Main Canvas/Buttons/Kolonya").GetComponent<SwitchButton>();
        SwitchButton dezenfektanButton = GameObject.Find("Main Canvas/Buttons/Dezenfektan").GetComponent<SwitchButton>();
        SwitchButton gazButton = GameObject.Find("Main Canvas/Buttons/Gas").GetComponent<SwitchButton>();

        checkLocked();

        switch (value)
        {
            case 0:
                sabunButton.selectedImage();
                break;
            case 1:
                sabunButton.notSelectedImage();
                kolonyaButton.selectedImage();
                break;
            case 2:
                sabunButton.notSelectedImage();
                dezenfektanButton.selectedImage();
                break;
            case 3:
                sabunButton.notSelectedImage();
                gazButton.selectedImage();
                break;
        }

        player.changeGunID(value);
    }

    private void checkLocked()
    {
        SwitchButton kolonyaButton = GameObject.Find("Main Canvas/Buttons/Kolonya").GetComponent<SwitchButton>();
        SwitchButton dezenfektanButton = GameObject.Find("Main Canvas/Buttons/Dezenfektan").GetComponent<SwitchButton>();
        SwitchButton gazButton = GameObject.Find("Main Canvas/Buttons/Gas").GetComponent<SwitchButton>();

        if (GameManager.Instance.kolonyaLocked)
        {
            kolonyaButton.lockedImage();
            kolonyaB.GetComponent<Button>().enabled = false;
            kA.text = "";
        }
        else
        {
            kolonyaButton.notSelectedImage();
            kolonyaB.GetComponent<Button>().enabled = true;
            kA.text = Kolonya.curAmmo.ToString();
        }

        if (GameManager.Instance.dezenfektanLocked)
        {
            dezenfektanButton.lockedImage();
            dezenfektanB.GetComponent<Button>().enabled = false;
            dA.text = "";
        }
        else
        {
            dezenfektanButton.notSelectedImage();
            dezenfektanB.GetComponent<Button>().enabled = true;
            dA.text = Dezenfektan.curAmmo.ToString();
        }

        if (GameManager.Instance.gasLocked)
        {
            gazButton.lockedImage();
            gasB.GetComponent<Button>().enabled = false;
            gDA.text = "";
        }
        else
        {
            gazButton.notSelectedImage();
            gasB.GetComponent<Button>().enabled = true;
            gDA.text = Gaz.curAmmo.ToString();
        }
    }

    public void updateScore()
    {
        scoreText.text = GameManager.Instance.score.ToString();
    }

    public void updateGold()
    {
        goldText.text = GameManager.Instance.inGameCoins.ToString();
    }

    public void updateAmmo(string gunType)
    {
        switch (gunType)
        {
            case "Kolonya":
                if (!GameManager.Instance.kolonyaLocked)
                {
                    kA.text = Kolonya.curAmmo.ToString();
                }
                break;
            case "Dezenfektan":
                if (!GameManager.Instance.dezenfektanLocked)
                {
                    dA.text = Dezenfektan.curAmmo.ToString();
                }
                break;
            case "Gaz Dezenfektan":
                if (!GameManager.Instance.gasLocked)
                {
                    gDA.text = Gaz.curAmmo.ToString();
                }
                break;
        }
    }

    public void fillAllAmmos()
    {
        Kolonya.maxAmmo = (GameManager.Instance.kolonyaLevel + 1) * 10;
        Dezenfektan.maxAmmo = (GameManager.Instance.dezenfektanLevel + 1) * 12;
        Gaz.maxAmmo = (GameManager.Instance.gasLevel + 1) * 18;

        Kolonya.fillAmmo();
        Dezenfektan.fillAmmo();
        Gaz.fillAmmo();

        updateAmmo("Kolonya");
        updateAmmo("Dezenfektan");
        updateAmmo("Gaz Dezenfektan");
    }

    public void playAgain()
    {
        GameManager.Instance.restartValues();
        LevelManager.Instance.openGame();
    }

    public void revieve()
    {
        closeGameOverPanel();
        foreach(Enemy enemy in GameManager.Instance.enemyList)
        {
            Destroy(enemy.gameObject);
        }
        GameManager.Instance.gameOver = false;
        Time.timeScale = 1;
    }

    public void returnMainMenu()
    {
        LevelManager.Instance.MainMenu();
    }
}
