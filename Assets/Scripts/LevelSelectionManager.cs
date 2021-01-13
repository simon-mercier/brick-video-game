using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelSelectionManager : MonoBehaviour
{
    [Header("BUTTON PREFAB")]
    [SerializeField] 
    private GameObject buttonPrefab;

   private void Start()
   {
       CreateButtons();
   }

    private void CreateButtons()
    {
        for (var i = 1; i <= LevelManager.Instance.NUMBER_OF_LEVELS; i++) 
        {
            var buttonGameObject = Instantiate(buttonPrefab);
            buttonGameObject.name = "Level Button " + i;
            buttonGameObject.GetComponent<LevelSelectionButton>().Level = i;
            buttonGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Level " + i;
            buttonGameObject.transform.SetParent(gameObject.transform, false);
        }
    }
}
