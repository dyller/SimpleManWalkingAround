using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CoinModel 
{
    public static CoinModel instance = null;
    private Boolean pause = false;
    private int coins = 0;
    public static CoinModel getInstance()
    {
        if (instance == null)
        {
            
                    instance = new CoinModel();
        }
        return instance;
    }
    public int AmountOfCOins()
    {
        return coins;
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
}
