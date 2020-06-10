﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinscript : MonoBehaviour
{
    CoinModel coinModel;
    float terrainStartX = -100;
    float terrainStartY = 2.02f;
    float terrainStartZ = -94.88f;
    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        coinModel = CoinModel.getInstance();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Then something hit coins
    private void OnTriggerEnter(Collider colider)
    {
        // destroy if car or player pick it 
        if (colider.name.Equals("Player") || colider.name.Equals("ColliderBodyCar")
            || colider.name.Equals("ColliderBottomCar") || colider.name.Equals("ColliderFrontCar"))
        {
            coinModel.AddCoin();
            var go = GameObject.Find("Terrain");
            Terrain terrain = go.GetComponent<Terrain>();
            float spawnX = Random.Range(0,terrain.terrainData.size.x);
            float spawnZ = Random.Range(0, terrain.terrainData.size.z);
            GameObject game = gameObject.transform.parent.gameObject;
            coin.transform.position = new Vector3(terrainStartX + spawnX, terrainStartY, terrainStartZ + spawnZ);
            GameObject childObject = Instantiate(coin) as GameObject;
            childObject.transform.parent = game.transform;


            Destroy(gameObject);
            
        }
    }
   
}
