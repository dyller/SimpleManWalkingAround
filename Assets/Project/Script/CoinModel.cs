using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;

public class CoinModel 
{
    public static CoinModel instance = null;
    private Boolean pause = false;
    private int amountOfCoins = 1;
    private int coins = 0;
    public static CoinModel getInstance()
    {
        if (instance == null)
        {
            
                    instance = new CoinModel();
        }
        return instance;
    }
    public int AmountOfCoins()
    {
        return coins;
    }
    public void UseCoins(int usedCoins)
    {
        coins -= usedCoins;
    }
    public void AddCoin()
    {
        coins += 1;
    }
    public void PauseGame()
    {
        pause = !pause;
    }
    public Boolean IsGamePaused()
    {
        return pause;
    }
    public void IncreaseCoinsAmount()
    {
        amountOfCoins++;
    }
    public int AmountOfSpawnCoins()
    {
        return amountOfCoins;
    }
}
