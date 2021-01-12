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
    private ButtonManager _ButtonManager;
    private static bool canChangePage = false;
    public static bool currentyLoadingLevel = false;

    private void Awake()
    {
        swipeTouch = gameObject.GetComponent<SwipeTouch>();
        gameObject.SetActive(true);
        levelManager = FindObjectOfType<LevelManager>();
    }
    private void Update()
    {
        if (!swipeTouch)
            return;
        if (swipeTouch.SwipeLeft)
            OnClick_RArrow();
        else if(swipeTouch.SwipeRight)
            OnClick_LArrow();
    }
   

    public void OnClick_Level()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

        if (Convert.ToInt32(gameObject.name.Substring(12)) <= LevelManager.maxLevel)
        {
            if (!currentyLoadingLevel)
            {
                currentyLoadingLevel = true;
                transform.parent.parent.parent.GetComponent<Animator>().SetTrigger("Out");
                Invoke("OnWait_Level", 1);
            }
        }
    }

    public void OnWait_Level()
    {

        LevelManager.currentLevel = Convert.ToInt32(gameObject.name.Substring(12));
        levelManager.SpawnLevel();
        transform.parent.parent.parent.gameObject.SetActive(false);

    }


    public void OnClick_Help()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

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
                        MoveBrick.CanMove = true;
                    }
                    else
                    {
                        child.SetActive(true);
                        canChangePage = false;
                        MoveBrick.CanMove = false;
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

        _DetectGameOver = FindObjectOfType<DetectGameOver>();
        StartCoroutine(_DetectGameOver.DestroyLevel(0f));
    }

    public void OnClick_Play()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

        canChangePage = true;

        levelSelector.SetActive(true);
        levelSelector.transform.GetChild(0).gameObject.SetActive(false);
        levelSelector.transform.GetChild(0).gameObject.SetActive(true);

        transform.parent.gameObject.SetActive(false);

        BannerTextController.ChangeName();
    }

    public void OnClick_Resume()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        canChangePage = true;
        OnClick_Settings();
    }

    public void OnClick_Restart()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        if (!GameObject.Find("Levels"))
        {
            _MakeMap.Restart();
            OnClick_Settings();
        }
    }

    public void OnClick_RestartLevelFail()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        _MakeMap.Restart();
    }

    public void OnClick_RestartLevelSuccess()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

        if (LevelManager.currentLevel >= LevelManager.maxLevel)
        {
            LevelManager.maxLevel = LevelManager.currentLevel + 1;
            PlayerPrefs.SetInt("CurrentLevel", LevelManager.maxLevel);
        }
        gameObject.transform.parent.gameObject.SetActive(false);
        _MakeMap.Restart();
    }

    public void OnClick_Elsewhere()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        OnClick_Settings();
    }

    public void OnClick_Elsewhere_Help()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        OnClick_Help();
    }
    
    public void OnClick_LArrow()
    {
        if (ButtonManager.page != 0 && canChangePage)
        {
            if (AudioManager.SoundOn)
                FindObjectOfType<AudioManager>().Play("Click");
            StartCoroutine(DoLeftAnim());
        }
    }

    public void OnClick_RArrow()
    {
        _ButtonManager = FindObjectOfType<ButtonManager>();

        if (ButtonManager.page == (Math.Floor((double) _ButtonManager.buttonPrefabs.Count / 21) - 1) || !canChangePage)
            return;
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        StartCoroutine(DoRightAnim());
    }

    public void OnClick_Selector()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        if (_MakeMap != null)
        {
            _MakeMap.DestroyLevel();
        }

        startScreen.SetActive(true);
        canChangePage = false;
        BannerTextController.ChangeName();

    }

    public void OnClick_Sound()
    {
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");

        AudioManager.SoundOn = !AudioManager.SoundOn;
        if (AudioManager.SoundOn)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SoundOn");
            PlayerPrefs.SetInt("Sound", 0);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SoundOff");
            PlayerPrefs.SetInt("Sound", 1);
        }
    }
    public void OnClick_Levels()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
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
        if (AudioManager.SoundOn)
            FindObjectOfType<AudioManager>().Play("Click");
        levelSelector = GameObject.Find("UI").transform.GetChild(0).gameObject;
        levelSelector.SetActive(true);
        GameObject.Find("EndGameScreen").SetActive(false);
        _MakeMap.DestroyLevel();
        Invoke("ChangeName", .2f);
    }

    private void ChangeName()
    {
        BannerTextController.ChangeName();
    }

    

    IEnumerator DoLeftAnim()
    {
        _ButtonManager = FindObjectOfType<ButtonManager>();
        yield return new WaitForSeconds(0f);
        _ButtonManager.ChangePageLevelSelector(false);
        _animator.SetTrigger("FadeIn");

        ButtonManager.page--;
        _ButtonManager.ChangePageLevelSelector(true);
        BannerTextController.ChangeName();
        /*
        if (ButtonManager.page == 0)
            _ButtonManager.LArrow.SetActive(false);
        _ButtonManager.RArrow.SetActive(true);*/

    }
    
    IEnumerator DoRightAnim()
    {
        _ButtonManager = FindObjectOfType<ButtonManager>();
        yield return new WaitForSeconds(0f);
        _ButtonManager.ChangePageLevelSelector(false);
        _animator.SetTrigger("FadeIn");

        ButtonManager.page++;
        _ButtonManager.ChangePageLevelSelector(true);
        BannerTextController.ChangeName();
        /*
        if (ButtonManager.page == Math.Floor(((double)_ButtonManager.buttonPrefabs.Count / 21) - 1))
            _ButtonManager.RArrow.SetActive(false);
        _ButtonManager.LArrow.SetActive(true);*/
    }
    
     
}
