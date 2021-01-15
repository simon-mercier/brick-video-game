using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Level
{
    private List<GameObject> tiles;

    private ILookup<Type, GameObject> groupedTile; 

    public GameObject Brick { get; private set; } = null;
    public int LevelNumber { get; private set; }

    public GameObject LevelGameObject { get; set; } = null;
    public Level(int level)
    {
        tiles = new List<GameObject>();
        this.LevelNumber = level;
        GenerateLevel();
        AddBrick();
    }

    public void ActivateHiddenCheckPoint()
    {
        foreach (var hiddenTile in groupedTile[typeof(HiddenTile)])
            hiddenTile.SetActive(true);
    }

    public void ActivateHiddenTempCheckPoint()
    {
        foreach (var hiddenTile in groupedTile[typeof(HiddenTempTile)])
        {
            hiddenTile.SetActive(false);
            hiddenTile.SetActive(true);
        }
    }

    private void SetCameraPosition(int width, int heigth)
    {
        Camera.main.transform.position = new Vector3((width / 2f) - 0.5f, -((heigth+5)*Mathf.Cos(Camera.main.transform.eulerAngles.x)), ((heigth / 2f)+5) * Mathf.Sin(Camera.main.transform.eulerAngles.x));
    }

    private void AddBrick()
    {
        Brick = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Brick/Brick"), new Vector3(0, 3, 0), Quaternion.identity);
    }

    private void GenerateLevel()
    {
        LevelGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BaseLevel")) as GameObject;
        var levelData = Serilization.DeserializeLevel(LevelNumber);
        
        for (int row = 0; row < levelData.Width; row++)
        {
            for (int col = 0; col < levelData.Height; col++)
            {
                var tile = levelData.Data[row, col];

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
        groupedTile = tiles.ToLookup(x => x.GetComponent<Tile>().GetType());

        SetCameraPosition(levelData.Width, levelData.Height);
    }
}