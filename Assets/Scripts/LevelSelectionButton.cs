using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionButton : SingleClickButton
{
    protected override void OnClick()
    {
        SceneManager.LoadScene(Scenes.LevelSelectionScene, LoadSceneMode.Single);
    }
}
