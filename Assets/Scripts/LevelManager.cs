using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public sealed class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance => instance;
   
    private int LastUnlockedLevel { get; set; }

    public readonly int NUMBER_OF_LEVELS = 84;

    private Level currentLevel = null;

    private void Awake()
    {
        //Serilization.SerializeLevels();
        instance ??= this;
        LoadLevel(1);
    }

    public void LoadLevel(int level)
    {
        currentLevel = new Level(level);
    }

    public void LevelCompleted()
    {
        if (currentLevel.LevelNumber >= NUMBER_OF_LEVELS)
            return;

        if(currentLevel.LevelNumber >= LastUnlockedLevel)
            PlayerPrefs.SetInt("CurrentLevel", ++LastUnlockedLevel);

        currentLevel = null;
    }

    public void LevelFailed()
    {
        currentLevel = null;
    }
}

class Level
{
    private List<GameObject> tiles;
    public int LevelNumber { get; private set; }

    public GameObject LevelGameObject { get; set; } = null;
    public Level(int level)
    {
        tiles = new List<GameObject>();
        this.LevelNumber = level;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        LevelGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BaseLevel"), GameObject.Find("Level").transform) as GameObject;
        var data = Serilization.DeserializeLevel(LevelNumber);
        var baseLevelTransform = LevelGameObject.transform;
        
        for (int row = 0; row < data.GetLength(0); row++)
        {
            for (int col = 0; col < data.GetLength(1); col++)
            {
                var tile = data[row, col];

                switch (tile)
                {
                    case 't':
                        tiles.Add(GameObject.Instantiate(NormalTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 's':
                        tiles.Add(GameObject.Instantiate(StartTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'e':
                        tiles.Add(GameObject.Instantiate(EndTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'q':
                        tiles.Add(GameObject.Instantiate(SandTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'a':
                        tiles.Add(GameObject.Instantiate(CheckPointTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'h':
                        tiles.Add(GameObject.Instantiate(HiddenTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'c':
                        tiles.Add(GameObject.Instantiate(CkeckPointTempTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'p':
                        tiles.Add(GameObject.Instantiate(HiddenTempTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case 'f':
                        tiles.Add(GameObject.Instantiate(FlashTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;
                    case '0':
                        tiles.Add(GameObject.Instantiate(ColliderTile.Tile, new Vector3(row, -1.5f, col), Quaternion.identity, LevelGameObject.transform));
                        break;

                }
            }
        }

        var brick = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Brick/Brick"), new Vector3(0, 3, 0), Quaternion.identity) as GameObject;
        brick.SetActive(true);
    }
}