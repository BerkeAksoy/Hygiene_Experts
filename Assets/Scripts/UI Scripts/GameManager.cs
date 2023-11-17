using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public List<Enemy> enemyList;
    public int heldCoins, inGameCoins, sabunLevel, kolonyaLevel, dezenfektanLevel, gasLevel, highestScore, score;
    public Gun[] guns;
    public bool gameOver, kolonyaLocked = true, dezenfektanLocked = true, gasLocked = true, noAdsBought;
    private float levelTime, startTime, partyElapsedTime, partyStartTime;
    public string selectedLanguage;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Game Manager is null.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            enemyList = new List<Enemy>();
            bool loadSuccessful = loadGame();
            //Screen.SetResolution()

            if (!loadSuccessful)
            {
                selectedLanguage = "English";
                gameOver = false;
                kolonyaLocked = true;
                dezenfektanLocked = true;
                gasLocked = true;
                heldCoins = 0;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void updateScore(float value)
    {
        score += (int)value;

        if(score > highestScore)
        {
            highestScore = score;
        }

        GameObject.Find("Main Canvas").GetComponent<GameUIManager>().updateScore();
    }

    public void restartValues()
    {
        gameOver = false;
        inGameCoins = 0;
        score = 0;
        Time.timeScale = 1;

        Kolonya.fillAmmo();
        Dezenfektan.fillAmmo();
        Gaz.fillAmmo();

        startLevelTimer();
        startPartyTimer();
    }

    public void saveGame()
    {
        SaveSystem.SavePlayer(this);
    }

    public bool loadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (data != null)
        {
            heldCoins = data.heldCoins;
            sabunLevel = data.sabunLevel;
            kolonyaLevel = data.kolonyaLevel;
            dezenfektanLevel = data.dezenfektanLevel;
            gasLevel = data.gasLevel;
            highestScore = data.highestScore;

            selectedLanguage = data.preferredLanguage;

            kolonyaLocked = data.kolonyaLocked;
            dezenfektanLocked = data.dezenfektanLocked;
            gasLocked = data.gasLocked;
            noAdsBought = data.noAdsBought;

            return true;
        }
        else
        {
            return false;
        }
    }

    public void startLevelTimer()
    {
        startTime = Time.time;
    }

    public void startPartyTimer()
    {
        partyStartTime = Time.time;
    }

    public float getElapsedPartyTime()
    {
        partyElapsedTime = Time.time - partyStartTime;
        return partyElapsedTime;
    }

    public float getElapsedLevelTime()
    {
        levelTime = Time.time - startTime;
        return levelTime;
    }

    public bool isGameOver()
    {
        return gameOver;
    }


}
