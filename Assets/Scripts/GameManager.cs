using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;

    private void Awake()
    {
        DontDestroyOnLoad(instance ??= this);
        

        if (PlayerPrefs.GetInt("LastUnlockedLevel") == 0)
            PlayerPrefs.SetInt("LastUnlockedLevel", 1);

    }
}
