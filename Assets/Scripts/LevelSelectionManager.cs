using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("BUTTON PREFAB")]
    [SerializeField]
    private GameObject buttonPrefab;

    private void Awake()
    {
        CreateButtons();
    }

    public void CreateButtons()
    {
        if (SceneManager.GetActiveScene().path != Scenes.LevelSelectionScene)
            return;

        for (var i = 1; i <= LevelManager.NUMBER_OF_LEVELS; i++)
            Instantiate(buttonPrefab, gameObject.transform).GetComponent<LevelButton>().Level = i;
    }
}
