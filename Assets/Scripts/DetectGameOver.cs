using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DetectGameOver : MonoBehaviour
{
    private GameObject brick;

    private MakeMap makeMap;

    private LevelManager levelManager;

    private static bool adRollTurn = false;

    private const float TIME_TEMP_TILE = 10f;

    private static DetectGameOver instance;

    void Awake()
    {
        makeMap = GetComponent<MakeMap>();
        instance = this;
        LevelSuccess = GameObject.Find("UI").transform.GetChild(3).gameObject;
        levelManager = FindObjectOfType<LevelManager>();

    }

    void FixedUpdate()
    {
        brick = GameObject.Find("Brick(Clone)");
        MoveDetection();

    }

    private void MoveDetection()
    {
        DetectIfPlayerEndTile();
        //DetectIfPlayerStartTile();
        DetectHiddenActivator();
        DetectHiddenTempActivator();
        DetectOnArrow();
        DetectBrickFallen();
        DetectIfActiveForCollider();
    }

    private void DetectIfActiveForCollider()
    {
        for (var i = 0; i < makeMap.HiddenTiles.Count; i++)
            makeMap.HiddenTilesColliders[i].SetActive(!makeMap.HiddenTiles[i].activeSelf);
        for (var i = 0; i < makeMap.HiddenTempTiles.Count; i++)
            makeMap.HiddenTempTilesColliders[i].SetActive(!makeMap.HiddenTempTiles[i].activeSelf);
        for (var i = 0; i < makeMap.QuickSandTempColliders.Count; i++)
            makeMap.QuickSandTempColliders[i].SetActive(!makeMap.QuickSandColliders[i].activeSelf);
        for (var i = 0; i < makeMap.FlashTilesColliders.Count; i++)
            makeMap.FlashTilesColliders[i].SetActive(!makeMap.FlashTiles[i].activeSelf);
    }

    private bool DetectIfIsUpOnTile(List<GameObject> tilesType)
    {
        return tilesType.Any(DetectIfIsUpOnTile);
    }

    private bool DetectIfIsUpOnTile(GameObject tilesType)
    {
        return (brick.transform.position.y <= tilesType.transform.position.y + 1.5f && brick.transform.position.x <= tilesType.transform.position.x + .2f && brick.transform.position.x >= tilesType.transform.position.x - .2f && brick.transform.position.z <= tilesType.transform.position.z + .2f && brick.transform.position.z >= tilesType.transform.position.z - .2f);
    }

    private void DetectBrickFallen()
    {
        if (brick.transform.position.y < -25)
        {
            MakeMap.RestartLevel();
        }
    }


    private void DetectIfPlayerEndTile()
    {
        if (!DetectIfIsUpOnTile(makeMap.EndTilePos) || !MoveBrick.CanMove) return;
        if (MoveBrick.CanPlaySound && AudioManager.SoundOn)
        {
            AudioManager.Play(Sounds.Win);
            MoveBrick.CanPlaySound = false;
        }

        MoveBrick.CanMove = false;

        if (!FirstTimeInLoop) return;
        FirstTimeInLoop = false;
        if (LevelManager.currentLevel == LevelManager.numberOfLevels)
        {
            GameObject.Find("UI").transform.GetChild(5).gameObject.SetActive(true);
        }
        else
        {
            ChangeCurrentLevel();
            LevelSuccess.SetActive(true);
        }
    }

   
    public void ChangeCurrentLevel()
    {
        if (LevelManager.currentLevel < LevelManager.maxLevel ||
            LevelManager.maxLevel > LevelManager.numberOfLevels) return;
        PlayerPrefs.SetInt("CurrentLevel", LevelManager.maxLevel);
        LevelManager.maxLevel = LevelManager.currentLevel;


    }


    private void DetectIfPlayerStartTile()
    {
        if (!DetectIfIsUpOnTile(makeMap.StartTilePos)) return;
        if (MoveBrick.CanPlaySound && AudioManager.SoundOn)
        {
            AudioManager.Play(Sounds.Switch);
            MoveBrick.CanPlaySound = false;
        }
        makeMap.Tiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.HiddenTempTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.HiddenTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.QuickSandTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.HiddenTiles.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.HiddenTileActivators.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
        makeMap.HiddenTempTileActivators.ForEach(a => a.GetComponent<MeshRenderer>().enabled = true);
    }

    private void DetectHiddenActivator()
    {
        if (!DetectIfIsUpOnTile(makeMap.HiddenTileActivators)) return;
        CanPressCheckPoint = false;
        if (MoveBrick.CanPlaySound && AudioManager.SoundOn)
        {
            AudioManager.Play(Sounds.Switch);
            MoveBrick.CanPlaySound = false;
        }
        MakeMap.SetTilesActiveState(true, makeMap.HiddenTiles);
    }


    private void DetectOnArrow()
    {
        if (DetectIfIsUpOnTile(MakeMap.arrows))
        {
            MakeMap.arrows.ForEach(a => a.SetActive(false));
        }
    }

    float timeLeft = 0;

    public GameObject LevelSuccess { get; set; }

    public bool FirstTimeInLoop { get; set; } = true;

    public bool CanPressCheckPoint { get; set; } = true;

    public bool BrickHasMoved { get; set; } = false;

    private void DetectHiddenTempActivator()
    {
        timeLeft -= Time.deltaTime;

        if (!DetectIfIsUpOnTile(makeMap.HiddenTempTileActivators) || !(timeLeft < 0)) return;
        timeLeft = .5f;

        if (MoveBrick.CanPlaySound && AudioManager.SoundOn)
        {
            AudioManager.Play(Sounds.Switch);
            MoveBrick.CanPlaySound = false;
        }
        MakeMap.SetTilesActiveState(false, makeMap.HiddenTempTiles);
        MakeMap.SetTilesActiveState(true, makeMap.HiddenTempTiles);
            
        CancelInvoke("ResetTempTiles");
        Invoke("ResetTempTiles", TIME_TEMP_TILE);
    }

    private void ResetTempTiles()
    {
        MakeMap.SetTilesActiveState(false, makeMap.HiddenTempTiles);
    }

    public IEnumerator DestroyLevel(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        LevelSuccess.gameObject.SetActive(false);

        makeMap.DestroyLevel();

        LevelManager.currentLevel++;
        if (LevelManager.currentLevel > LevelManager.maxLevel)
            LevelManager.maxLevel = LevelManager.currentLevel;

        PlayerPrefs.SetInt("CurrentLevel", LevelManager.maxLevel);

        levelManager.SpawnLevel();

        BannerTextController.ChangeName();

    }
}
