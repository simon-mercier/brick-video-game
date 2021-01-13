using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnClick_Play()
    {
        AudioManager.Play(Sounds.Click);
        SceneManager.LoadScene(Scene.LevelSelectionScenePath, LoadSceneMode.Single);
    }
}
