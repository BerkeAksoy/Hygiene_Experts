using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int heldCoins, sabunLevel, kolonyaLevel, dezenfektanLevel, gasLevel, highestScore;
    public string preferredLanguage;
    public bool kolonyaLocked, dezenfektanLocked, gasLocked, noAdsBought;

    public PlayerData (GameManager gM)
    {
        heldCoins = gM.heldCoins;
        sabunLevel = gM.sabunLevel;
        kolonyaLevel = gM.kolonyaLevel;
        dezenfektanLevel = gM.dezenfektanLevel;
        gasLevel = gM.gasLevel;
        highestScore = gM.highestScore;

        preferredLanguage = gM.selectedLanguage;

        kolonyaLocked = gM.kolonyaLocked;
        dezenfektanLocked = gM.dezenfektanLocked;
        gasLocked = gM.gasLocked;
        noAdsBought = gM.noAdsBought;
    }



}
