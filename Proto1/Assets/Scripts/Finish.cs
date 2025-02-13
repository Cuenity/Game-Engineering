﻿using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Finish : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction Finished;

    GameState gameState;
    PlayerManager playerManager;
    public Vector3 spawnpoint;

    // Use this for initialization
    void Start()
    {
        gameState = GameState.Instance;
        playerManager = gameState.GetComponent<PlayerManager>();
        gameState.levelManager.finish = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        //playerManager.collectedCoins++;
        if (!other.isTrigger)
        {
            Finished();
            //prevent het oneindig finishen van een level (in singleplayer maakt dit niet uit want start dan altijd een andere scene
            if(!PhotonNetwork.InRoom)
                this.gameObject.SetActive(false);
        }

    }
}
