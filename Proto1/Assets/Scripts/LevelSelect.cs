﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    Canvas worldSelectCanvas;

    [SerializeField]
    Canvas levelSelectCanvas;

    [SerializeField]
    Canvas levelSelectCanvasWorld2;
    [SerializeField]
    Canvas levelSelectCanvasWorld3;
    [SerializeField]
    Sprite world1Image, world2Image, world3Image;

    [SerializeField]
    Sprite stars0;
    [SerializeField]
    Sprite stars1;
    [SerializeField]
    Sprite stars2;
    [SerializeField]
    Sprite stars3;
    [SerializeField]
    Sprite stickerCollected;
    [SerializeField]
    Sprite levelCompleted;


    [SerializeField]
    Text stickersCollectedText;
    [SerializeField]
    Text stickersCollectedTextWorld2;
    [SerializeField]
    Text stickersCollectedTextWorld3;

    //Dit moet later weg is vieze oplossing voor viez probleem


    Image[] stickerIndicators;
    [SerializeField]
    Image[] completedIndicators;





    int stickersCollectedCount;
    List<Button> LevelSelectButtons = new List<Button>();
    PlayerData player;
    bool textIsUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        levelSelectCanvas.gameObject.SetActive(false);
        levelSelectCanvasWorld2.gameObject.SetActive(false);

        player = PlayerDataController.instance.Player;
    
        foreach (var button in levelSelectCanvas.GetComponentsInChildren<Button>())
        {
            if (!button.name.Contains("Back"))
                LevelSelectButtons.Add(button);
        }
        foreach (var button in levelSelectCanvasWorld2.GetComponentsInChildren<Button>())
        {
            if (!button.name.Contains("Back"))
                LevelSelectButtons.Add(button);
        }
        foreach (var button in levelSelectCanvasWorld3.GetComponentsInChildren<Button>())
        {
            if (!button.name.Contains("Back"))
                LevelSelectButtons.Add(button);
        }


        for (int i = 0; i < player.levels.Length; i++)
        {
            if (player.levels[i].gotSticker)
            {
                stickersCollectedCount++;
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!textIsUpdated)
        {
            // Dit moet nog afgemaakt worden
            stickersCollectedText.text = stickersCollectedText.text + " " + stickersCollectedCount.ToString();
            stickersCollectedTextWorld2.text = stickersCollectedTextWorld2.text + " " + stickersCollectedCount.ToString() + "/5";
            stickersCollectedTextWorld3.text = stickersCollectedTextWorld3.text + " " + stickersCollectedCount.ToString() + "/10";
            if (stickersCollectedCount > 4)
            {
                Button[] worldbutton = this.worldSelectCanvas.GetComponentsInChildren<Button>();
                worldbutton[1].GetComponent<Image>().sprite = world2Image;
                stickersCollectedTextWorld2.gameObject.SetActive( false);
            }
            if (stickersCollectedCount > 9)
            {
                Button[] worldbutton = this.worldSelectCanvas.GetComponentsInChildren<Button>();
                worldbutton[2].GetComponent<Image>().sprite = world3Image;
                stickersCollectedTextWorld3.gameObject.SetActive(false);
            }
           
            textIsUpdated = true;
        }
    }

    void LoadCorrectPicturesForLevels()
    {
        for (int i = 0; i < 30; i++)
        {
            switch (player.levels[i].countCoins)
            {
                case (0):
                    LevelSelectButtons[i].image.sprite = stars0;
                    break;
                case (1):
                    LevelSelectButtons[i].image.sprite = stars1;
                    break;
                case (2):
                    LevelSelectButtons[i].image.sprite = stars2;
                    break;
                case (3):
                    LevelSelectButtons[i].image.sprite = stars3;
                    break;

                default:
                    LevelSelectButtons[i].image.sprite = stars0;
                    break;
            }
            Image sticker = LevelSelectButtons[i].transform.Find("Sticker").GetComponent<Image>();
            switch (player.levels[i].gotSticker)
            {
                case (true):
                    sticker.sprite = stickerCollected;
                    break;
                case (false):
                    sticker.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
            Image ding = LevelSelectButtons[i].transform.Find("Completed").GetComponent<Image>();
            switch (player.levels[i].completed)
            {
                case (true):
                    ding.sprite = levelCompleted;
                    break;
                case (false):
                    ding.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }


        }

    }

    public void LaadLevel(string level)
    {
        PlayerDataController.instance.PreviousScene = Convert.ToInt32(level);
        worldSelectCanvas.gameObject.SetActive(false);
        levelSelectCanvas.gameObject.SetActive(false);
        SceneSwitcher.Instance.AsynchronousLoadStart("1");
    }
    public void ShowLevelSelect()
    {
        worldSelectCanvas.gameObject.SetActive(false);
        LoadCorrectPicturesForLevels();
        levelSelectCanvas.gameObject.SetActive(true);

    }

    public void BackToStartMenu()
    {
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("StartMenu");
    }

    public void BackToWorldSelect()
    {
        worldSelectCanvas.gameObject.SetActive(true);
        levelSelectCanvas.gameObject.SetActive(false);
    }

    public void World2()
    {
        if (stickersCollectedCount > 4)
        {
            ShowLevelSelectWorld2();
        }
    }
    public void World3()
    {
        if (stickersCollectedCount > 9)
        {
            ShowLevelSelectWorld3();
        }
    }

    public void ShowLevelSelectWorld2()
    {
        worldSelectCanvas.gameObject.SetActive(false);
        LoadCorrectPicturesForLevels();
        levelSelectCanvasWorld2.gameObject.SetActive(true);
    }
    public void ShowLevelSelectWorld3()
    {
        worldSelectCanvas.gameObject.SetActive(false);
        LoadCorrectPicturesForLevels();
        levelSelectCanvasWorld3.gameObject.SetActive(true);
    }
}
