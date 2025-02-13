﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviourPun
{
    // Start is called before the first frame update
    GameState gameState = GameState.Instance;
    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
        Debug.Log(this.GetComponent<Rigidbody>().mass);
    }
    void Start()
    {
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;

    }


    private void FixedUpdate()
    {
        if (this.transform.position.y < gameState.gridManager.height * -1 || this.transform.position.x < 0 || this.transform.position.x > gameState.gridManager.width)
        {
            if (PlayerPrefs.GetInt("Vibration") == 1)
            {
                Handheld.Vibrate();
            }
            gameState.levelManager.SetBuildingPhase();
        }
    }
    #region rpcs(PhotonSyncers)
    [PunRPC]
    void FlagHit(string hitter)
    {
        if(hitter == "masterhit")
        {
            gameState.UIManager.ChangeFlagHitTrue(gameState.UIManager.p1FlagHit);
            gameState.levelManager.p1Finish = true;
        }
        else
        {
            gameState.UIManager.ChangeFlagHitTrue(gameState.UIManager.p2FlagHit);
            gameState.levelManager.p2Finish = true;
        }

        //checken of we gewonnen hebben 

        if(gameState.levelManager.p1Finish ==true && gameState.levelManager.p2Finish == true)
        {
            PhotonView view = this.GetComponent<PhotonView>();
            view.RPC("WinLevel", RpcTarget.All);
            Debug.Log("gewonnen in MP fucker");
        }
    }
    [PunRPC]
    void FlagUnHit(string hitter)
    {
        if (hitter == "masterhit")
        {
            gameState.UIManager.ChangeFlagHitFalse(gameState.UIManager.p1FlagHit);
            gameState.levelManager.p1Finish = false;
        }
        else
        {
            gameState.UIManager.ChangeFlagHitFalse(gameState.UIManager.p2FlagHit);
            gameState.levelManager.p2Finish = false;
        }
    }
    [PunRPC]
    void WinLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(7);
        }
    }
    [PunRPC]
    void BackToLevelSelect()
    {
        PhotonStartMenu.Instance.ComesFromLevel = true;
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(9);
        }
    }
    [PunRPC]
    void setActivePlayers(string playerMaster)
    {
        gameState.playerBallManager.MultiActivePlayer2 = gameState.playerBallManager.activePlayer;
        Debug.Log("multiactive2set");
        Debug.Log(gameState.playerBallManager.MultiActivePlayer2);
    }
    #endregion
}
