//using System;
//using System.Collections.Generic;
//using UnityEngine;

////#define ENABLE_UNITY_ADS

//#if ENABLE_UNITY_ADS
//using UnityEngine.Advertisements;
//#endif

//public class SkipLevel : MonoBehaviour {
    
    
//    DetectGameOver _DetectGameOver;

//    private void Awake()
//    {
//#if ENABLE_UNITY_ADS
//        if (!Advertisement.isInitialized)
//        {
//            Advertisement.Initialize("Brick", true);  //// 1st parameter is String and 2nd is boolean
//        }
//#endif
//    }
//    public void OnClick_SkipLevel()
//    {
//#if ENABLE_UNITY_ADS
//        if (AudioManager.SoundOn)
//            FindObjectOfType<AudioManager>().Play("Click");
//        if (Advertisement.IsReady())
//        {
//            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
//        }
//#endif
//    }

//    public void SkipLevelNotRewarded()
//    {
//#if ENABLE_UNITY_ADS
//        if (Advertisement.IsReady())
//        {
//            Advertisement.Show("video");
//        }
//#endif
//    }

//#if ENABLE_UNITY_ADS
//    private void HandleAdResult(ShowResult result)
//    {
//        switch (result)
//        {
//            case ShowResult.Finished:
//                LevelSkip();
//                break;
//        }
//    }
//#endif

//    public void LevelSkip()
//    {
//        if (LevelManager.currentLevel >= LevelManager.maxLevel && LevelManager.maxLevel < LevelManager.numberOfLevels)
//        {
//            PlayerPrefs.SetInt("CurrentLevel", LevelManager.maxLevel);
//            LevelManager.maxLevel = LevelManager.currentLevel;
//        }
//        _DetectGameOver = FindObjectOfType<DetectGameOver>();
//        _DetectGameOver.ChangeCurrentLevel();
//        StartCoroutine(_DetectGameOver.DestroyLevel(0f));
//    }
//}
