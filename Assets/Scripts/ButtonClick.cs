using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ButtonClick : MonoBehaviour
{

    private LevelManager levelManager;

    [Header("LEVEL SELECTOR\n")]
    [SerializeField]
    GameObject levelSelector;

    [Header("STARTSCREEN\n")]
    [SerializeField]
    GameObject startScreen;

    [Header("ANIMATOR\n")]
    [SerializeField]
    Animator _animator;

    private SwipeTouch swipeTouch;
    private MakeMap _MakeMap;
    private DetectGameOver _DetectGameOver;
   // private L _ButtonManager;
    public static bool canChangePage = false;
    public static bool currentyLoadingLevel = false;

    private void Awake()
    {
        swipeTouch = gameObject.GetComponent<SwipeTouch>();
        gameObject.SetActive(true);
        levelManager = FindObjectOfType<LevelManager>();
    }
   

    //public void OnClick_Level()
    //{
        
    //        AudioManager.Play(Sounds.Click);

    //    if (Convert.ToInt32(gameObject.name.Substring(12)) <= LevelManager.maxLevel)
    //    {
    //        if (!currentyLoadingLevel)
    //        {
    //            currentyLoadingLevel = true;
    //            transform.parent.parent.parent.GetComponent<Animator>().SetTrigger("Out");
    //            Invoke("OnWait_Level", 1);
    //        }
    //    }
    //}

    //public void OnWait_Level()
    //{

    //    LevelManager.currentLevel = Convert.ToInt32(gameObject.name.Substring(12));
    //    levelManager.SpawnLevel();
    //    transform.parent.parent.parent.gameObject.SetActive(false);

    //}


    public void OnClick_Help()
    {
        
        AudioManager.Play(Sounds.Click);
        foreach (Transform t in transform.parent)
        {
            if (t.name == "HELP")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i).gameObject;
                    if (child.activeSelf == true) child.SetActive(false);
                    else child.SetActive(true);
                }
            }
        }
    }

    public void OnClick_Help2()
    {
        
            AudioManager.Play(Sounds.Click);
        foreach (Transform t in transform.parent)
        {
            if (t.name == "HELP2")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i).gameObject;
                    if (child.activeSelf == true) child.SetActive(false);
                    else child.SetActive(true);
                }
            }
        }
    }

    public void OnClick_Help3()
    {
        
            AudioManager.Play(Sounds.Click);
        foreach (Transform t in transform.parent.parent)
        {
            if (t.name == "HELP")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i).gameObject;
                    if (child.activeSelf == true) child.SetActive(false);
                    else child.SetActive(true);
                }
            }
        }
    }

    public void OnClick_Help4()
    {
        
            AudioManager.Play(Sounds.Click);
        foreach (Transform t in transform.parent.parent)
        {
            if (t.name == "HELP2")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i).gameObject;
                    if (child.activeSelf == true) child.SetActive(false);
                    else child.SetActive(true);
                }
            }
        }

    }

    public void OnClick_Settings()
    {
        
            AudioManager.Play(Sounds.Click);

        foreach (Transform t in transform.root)
        {
            if (t.name == "Settings")
            {
                for (int i = 0; i < t.childCount; i++)
                {
                    var child = t.GetChild(i).gameObject;
                    if (child.activeSelf == true)
                    {
                        child.SetActive(false);
                        canChangePage = true;
                        BrickManager.CanMove = true;
                    }
                    else
                    {
                        child.SetActive(true);
                        canChangePage = false;
                        BrickManager.CanMove = false;
                    }

                }
                if (GameObject.Find("Levels") && GameObject.Find("RESTART"))
                {
                    GameObject.Find("RESTART - text").GetComponent<Text>().color = new Color(.5f, .5f, .5f);
                    GameObject.Find("RESTART - image").GetComponent<Image>().color = new Color(.5f, .5f, .5f);
                }
                else if (GameObject.Find("RESTART"))
                {
                    GameObject.Find("RESTART - text").GetComponent<Text>().color = new Color(1f, 248 / 255f, 248 / 255f);
                    GameObject.Find("RESTART - image").GetComponent<Image>().color = new Color(1f, 1f, 1f);
                }
            }
        }
    }

    public void OnClick_NextLevel()
    {
        
            AudioManager.Play(Sounds.Click);

        _DetectGameOver = FindObjectOfType<DetectGameOver>();
        StartCoroutine(_DetectGameOver.DestroyLevel(0f));
    }

    

    public void OnClick_Resume()
    {
        
            AudioManager.Play(Sounds.Click);
        canChangePage = true;
        OnClick_Settings();
    }

    public void OnClick_Restart()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);
        if (!GameObject.Find("Levels"))
        {
            _MakeMap.Restart();
            OnClick_Settings();
        }
    }

    public void OnClick_RestartLevelFail()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);
        _MakeMap.Restart();
    }

    public void OnClick_RestartLevelSuccess()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);

        //if (LevelManager.currentLevel >= LevelManager.maxLevel)
        //{
        //    LevelManager.maxLevel = LevelManager.currentLevel + 1;
        //    PlayerPrefs.SetInt("CurrentLevel", LevelManager.maxLevel);
        //}
        gameObject.transform.parent.gameObject.SetActive(false);
        _MakeMap.Restart();
    }

    public void OnClick_Elsewhere()
    {
        
            AudioManager.Play(Sounds.Click);
        OnClick_Settings();
    }

    public void OnClick_Elsewhere_Help()
    {
        
            AudioManager.Play(Sounds.Click);
        OnClick_Help();
    }
    

    public void OnClick_Selector()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);
        if (_MakeMap != null)
        {
            _MakeMap.DestroyLevel();
        }

        startScreen.SetActive(true);
        canChangePage = false;

    }

    
    public void OnClick_Levels()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);
        levelSelector = GameObject.Find("UI").transform.GetChild(0).gameObject;
        levelSelector.SetActive(true);
        if(GameObject.Find("Level").transform.childCount > 0)
        {
            _MakeMap.DestroyLevel();
        }
        OnClick_Settings();

        Invoke("ChangeName", .2f);



    }

    public void OnClick_Selector_EndGame()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        
            AudioManager.Play(Sounds.Click);
        levelSelector = GameObject.Find("UI").transform.GetChild(0).gameObject;
        levelSelector.SetActive(true);
        GameObject.Find("EndGameScreen").SetActive(false);
        _MakeMap.DestroyLevel();
        Invoke("ChangeName", .2f);
    }


    

}
