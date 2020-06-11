using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CoinViewController : MonoBehaviour
{
    public Text coinsText;
    public GameObject coinSpawnUpgrade;
    public string coinsMessage = "Coins: ";
    private string spawnMessage = "UpgradeSpawn: ";
    private CoinModel coinModel;
    private float transParrentShop = 0.3f;
    private Boolean gameChange = false;
    private int spawnPrice = 1;
    float terrainStartX = -100;
    float terrainStartY = 2.02f;
    float terrainStartZ = -94.88f;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        coinModel = CoinModel.getInstance();
        
    }
    private void SpawnCoin()
    {
        var go = GameObject.Find("Terrain");
        Terrain terrain = go.GetComponent<Terrain>();
        float spawnX = Random.Range(0, terrain.terrainData.size.x);
        float spawnZ = Random.Range(0, terrain.terrainData.size.z);
        GameObject camera = gameObject.transform.parent.gameObject;
        GameObject player = camera.transform.parent.gameObject;
        GameObject game = player.transform.parent.gameObject;
        GameObject childObject = Instantiate(coin) as GameObject;
        childObject.transform.parent = game.transform;
        childObject.transform.position = new Vector3(terrainStartX + spawnX, terrainStartY - 0.78f, terrainStartZ + spawnZ);
    }

    // Update is called once per frame
    void Update()
    {   
        coinsText.text = coinsMessage + coinModel.AmountOfCoins();
        if (coinModel.IsGamePaused() && !gameChange)
        {
            Shop();
        }
        else if (!coinModel.IsGamePaused() && gameChange)
        {
            Removeshop();
        }
        
    }
    //Hide shop
    private void Removeshop()
    {
        coinSpawnUpgrade.SetActive(false);
        Image shopBackground = this.gameObject.GetComponent<Image>();
        var tempColor = shopBackground.color;
        tempColor.a = 0;
        shopBackground.color = tempColor;
        gameChange = false;
    }
    //show shop
    private void Shop()
    {

        Image shopBackground = this.gameObject.GetComponent<Image>();
        var tempColor = shopBackground.color;
        tempColor.a = transParrentShop;
        shopBackground.color = tempColor;
        coinSpawnUpgrade.SetActive(true);
        Text spawnCoinsText = coinSpawnUpgrade.GetComponent<Text>();
        spawnCoinsText.text = spawnMessage + spawnPrice + " | " + coinModel.AmountOfSpawnCoins() +" -> "+ (coinModel.AmountOfSpawnCoins()+1);
        gameChange = true;
        
    }
    public void UpgradeSpawn()
    {
        if (coinModel.AmountOfCoins() >= spawnPrice)
        {
            coinModel.UseCoins(spawnPrice);
            spawnPrice++;
            coinModel.IncreaseCoinsAmount();
            Text spawnCoinsText = coinSpawnUpgrade.GetComponent<Text>();
            spawnCoinsText.text = spawnMessage + spawnPrice + " | " + coinModel.AmountOfSpawnCoins() + " -> " + (coinModel.AmountOfSpawnCoins() + 1);
            SpawnCoin();
        }
    }
}
