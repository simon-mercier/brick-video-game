using UnityEngine;
using TMPro;

public class LevelNumberLabel : MonoBehaviour
{
    private void Start()
    {
        var level = LevelManager.Instance?.GetLevelNumber();
        GetComponent<TextMeshProUGUI>().text = "Level " + level;
    }
}
