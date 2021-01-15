using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int Level { get; set; }

    private static readonly Color LOCKED_COLOR = new Color32(190, 80, 80, 255);
    private void Start()
    {
        name = "Level Button " + Level;
        GetComponentInChildren<TextMeshProUGUI>().text = "Level " + Level;
        transform.SetParent(gameObject.transform, false);

        if (Level > LevelManager.LastUnlockedLevel)
        {
            GetComponent<Image>().color = LOCKED_COLOR;
            GetComponent<UnityEngine.UI.Button>().interactable = false;
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void OnClick()
    {
        SceneManager.LoadScene(Scenes.GameScene, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        LevelManager.Instance.LoadLevel(Level);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.L))
        {
            OnClick();
        }
    }
}
