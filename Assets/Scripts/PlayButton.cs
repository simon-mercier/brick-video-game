using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : SingleClickButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene(Scenes.LevelSelectionScene, LoadSceneMode.Single);
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            OnClick();
        }
    }
}
