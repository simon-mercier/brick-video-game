using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : SingleClickButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene(Scenes.GameScene, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        LevelManager.Instance.LoadNextLevel();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.N))
        {
            OnClick();
        }
    }
}
