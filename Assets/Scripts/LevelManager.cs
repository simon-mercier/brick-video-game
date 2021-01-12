using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _LevelPrefab;

    public static Sprite[] mapsSprites;
    public static int maxLevel = 1; //from 1 to max
    public static int currentLevel; //from 1 to max
    public static int numberOfLevels;

    private void Awake()
    {
        GetSprites();
    }

    private void GetSprites()
    {
        mapsSprites = Resources.LoadAll<Sprite>("Sprites/Levels");
        numberOfLevels = mapsSprites.Length;
    }

    internal void SpawnLevel()
    {
        var thisLevel = Instantiate(_LevelPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        thisLevel.SetActive(true);

        thisLevel.name = "level " + currentLevel;
        thisLevel.GetComponent<MakeMap>().map = mapsSprites[currentLevel - 1];
        thisLevel.GetComponent<MakeMap>().CreateMap();

        thisLevel.transform.SetParent(GameObject.Find("Level").transform);
        ButtonClick.currentyLoadingLevel = false;

        BannerTextController.ChangeName();
    }
}
