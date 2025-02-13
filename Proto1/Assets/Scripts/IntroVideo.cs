﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
//using GameAnalyticsSDK;

[RequireComponent (typeof(AudioSource))]
public class IntroVideo : MonoBehaviour
{
    [SerializeField]
    public RawImage rawImage;
    [SerializeField]
    public VideoPlayer videoPlayer;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
        //GameAnalytics.Initialize();
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!videoPlayer.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        rawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

        WaitForSeconds wait = new WaitForSeconds(1);
        while(videoPlayer.isPlaying)
        {
            yield return wait;
            break;
        }
  
        if (PlayerDataController.instance.Load())
        {
            LocalizationManager.instance.GetLanguageSettings();
            SceneManager.LoadScene("StartMenu");
        }
        else
        {
            SceneManager.LoadScene("LanguageSelection");
        }
    }
}
