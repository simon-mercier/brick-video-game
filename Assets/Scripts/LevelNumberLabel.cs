using UnityEngine;
using TMPro;

public class LevelNumberLabel : MonoBehaviour
{
    private void Start()
    {
        int? level = LevelManager.Instance?.GetLevelNumber();
        GetComponent<TextMeshProUGUI>().text = "level " + level;
    }
}
