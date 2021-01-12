using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BannerTextController : MonoBehaviour
{
    private static BannerTextController instance;
    public static BannerTextController Instance => instance ??= new BannerTextController();

    private static string FindName()
    {
        if(GameObject.Find("Level").transform.childCount > 0)
        {
            return "Level " + LevelManager.currentLevel;
        }
        return "" + (ButtonManager.page+1) + " of 4";
    }


    public static void ChangeName()
    {
        Instance.GetComponent<TMP_Text>().text = FindName();
    }
    
}
