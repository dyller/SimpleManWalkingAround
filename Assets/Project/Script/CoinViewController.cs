﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinViewController : MonoBehaviour
{
    public Text coinsText;
    public string coinsMessage = "Coins: ";
    private CoinModel coinModel;
    // Start is called before the first frame update
    void Start()
    {
        coinModel = CoinModel.getInstance();
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = coinsMessage + coinModel.AmountOfCOins();
        
    }
}
