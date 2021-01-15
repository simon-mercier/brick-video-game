using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public sealed class LevelManager : MonoBehaviour
{
    private static LevelManager instance = null;
    public static LevelManager Instance => instance;

    private static int? lastUnlockedLevel = null;
    public static int LastUnlockedLevel { get => lastUnlockedLevel ??= PlayerPrefs.GetInt("LastUnlockedLevel"); set => lastUnlockedLevel = value; }

    public static readonly int NUMBER_OF_LEVELS = 84;

    private Level currentLevel = null;
    private int lastPlayedLevelNumber;

    private float TIME_UNTIL_CHANGE_SCENE = .5f;

    private void Awake()
    {
        lastPlayedLevelNumber = PlayerPrefs.GetInt("LastPlayedLevelNumber");
        instance = this.GetComponent<LevelManager>();
    }

    public void LoadLevel(int level)
    {
        if (level > lastUnlockedLevel)
        {
            Debug.LogError("Level was not unloked");
            return;
        }

        currentLevel = new Level(level);
        PlayerPrefs.SetInt("LastPlayedLevelNumber", level);
        lastPlayedLevelNumber = level;
    }

    public void RestartLevel()
    {
        LoadLevel(lastPlayedLevelNumber);
    }

    public void LoadNextLevel()
    {
        LoadLevel(lastPlayedLevelNumber + 1);
    }

    public void LevelCompleted()
    {
        Brick.GetComponent<BrickManager>().CanMove = false;
        Invoke("LevelCompletedLogic", TIME_UNTIL_CHANGE_SCENE);
    }

    private void LevelCompletedLogic()
    {
        if (currentLevel.LevelNumber >= NUMBER_OF_LEVELS)
        {
            currentLevel = null;
            SceneManager.LoadScene(Scenes.GameCompletedScene, LoadSceneMode.Single);
            return;
        }
            
        if (currentLevel.LevelNumber >= LastUnlockedLevel)
            PlayerPrefs.SetInt("LastUnlockedLevel", ++LastUnlockedLevel);

        currentLevel = null;

        SceneManager.LoadScene(Scenes.LevelSuccessScene, LoadSceneMode.Single);
    }

    public void LevelFailed()
    {
        Brick.GetComponent<BrickManager>().CanMove = false;
        Invoke("LevelFailedLogic", TIME_UNTIL_CHANGE_SCENE);
    }

    private void LevelFailedLogic()
    {
        currentLevel = null;
        SceneManager.LoadScene(Scenes.LevelFailScene, LoadSceneMode.Single);
    }

    public void ActivateHiddenCheckPoint()
    {
        currentLevel?.ActivateHiddenCheckPoint();
    }

    public void ActivateHiddenTempCheckPoint()
    {
        currentLevel?.ActivateHiddenTempCheckPoint();
    }

    public GameObject Brick => currentLevel?.Brick;

    public int GetLevelNumber()
    {
        return currentLevel.LevelNumber;
    }
}
