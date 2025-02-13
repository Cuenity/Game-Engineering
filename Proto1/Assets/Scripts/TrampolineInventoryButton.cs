﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TrampolineInventoryButton : InventoryButton
{
    [SerializeField]
    private GameObject trampoline;
    [SerializeField]
    private Sprite trampolineImage;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = trampolineImage;
        gameObject.name = InventoryButtonName.trampolineButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
    }

    public override GameObject SpawnPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = Instantiate(trampoline);

        GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdateTrampolinesLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }

    public override GameObject SpawnPhotonPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = PhotonNetwork.Instantiate("Photon Trampoline", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace--;

        if (GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }

        GameState.Instance.levelManager.playerPlatforms.UpdateTrampolinesLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }
}
