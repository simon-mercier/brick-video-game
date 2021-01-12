using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class ButtonManager : MonoBehaviour
{
    [Header("BUTTON PREFAB")]
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Sprite buttonCanPress;

    public List<GameObject> buttonPrefabs = new List<GameObject>();

    public static int page = 0;

    private GameObject[] level;

   // public static string starsPerLevel;

   private void Awake()
   {
       CreateButtons();

        //PlayerPrefs.DeleteAll();
        LevelManager.maxLevel = PlayerPrefs.GetInt("CurrentLevel");

        if (LevelManager.maxLevel <= 1)
        {
            LevelManager.maxLevel = 1;
        }
        else if(LevelManager.maxLevel > LevelManager.numberOfLevels)
        {
            LevelManager.maxLevel = LevelManager.numberOfLevels;
        }
   }

   private void FixedUpdate()
   {
       if (GameObject.Find("Level").transform.childCount != 0 || GameObject.Find("StartScreen")) return;
       for (var i = 0; i < LevelManager.numberOfLevels; i++)
       {
           if (i < LevelManager.maxLevel - 1)
           {
               buttonPrefabs[i].GetComponent<Image>().color = Color.white;
           }
           else if (i < LevelManager.maxLevel)
           {
               buttonPrefabs[i].GetComponent<Image>().color = Color.white;
           }
           else
           {
               buttonPrefabs[i].GetComponent<Image>().color = new Color(0.6f, 0.6f, 0.6f);
           }
       }
   }
    
    private void OrganizeLevelSelector()
    {
        buttonPrefabs.ForEach(a =>
        {
            if (a.GetComponent<RectTransform>().transform.localPosition.z > 0) a.SetActive(false);
        });
    }
    
    public void ChangePageLevelSelector(bool activeState)
    {
        buttonPrefabs.ForEach(a =>
        {
            if (a.GetComponent<RectTransform>().transform.localPosition.z == page*10) a.SetActive(activeState);
        });
    }

    private void CreateButtons()
    {
        var tempX = 0;
        var tempY = 0;
        var tempZ = 0;
        for (var i = 0; i < LevelManager.mapsSprites.Length; i++) 
        {
            GameObject currentButton;
            buttonPrefabs.Add(currentButton = Instantiate(buttonPrefab) as GameObject);
            currentButton.name = "Level Button" + (i + 1);
           
            if(tempX == 7)
            {
                tempX = 0;
                tempY++;
            }
            if(tempY == 3)
            {
                tempY = 0;
                tempZ++;
            }

            currentButton.GetComponent<RectTransform>().transform.position = new Vector3(-750 + (tempX * 250), 200 - (tempY * 250), tempZ*10);
            tempX++;
            currentButton.GetComponentInChildren<Text>().text = "" + (i + 1);
            currentButton.transform.SetParent(gameObject.transform, false);
        }
        OrganizeLevelSelector();
    }
}
