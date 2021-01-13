using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionButton : MonoBehaviour
{
    public int Level { get; set; }
    public void OnClick_Button()
    {
        LevelManager.Instance.LoadLevel(Level);
    }
}
