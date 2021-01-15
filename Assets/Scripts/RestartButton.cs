using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : SingleClickButton
{
    protected override void OnClick()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(Scenes.GameScene, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        LevelManager.Instance.RestartLevel();
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) || (Input.GetKeyUp(KeyCode.Return) && SceneManager.GetActiveScene().path == Scenes.LevelFailScene))
        {
            OnClick();
        }
    }
}
