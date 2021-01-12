using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundIcon : MonoBehaviour {

	void Start () 
    {
        AudioManager.SoundOn = PlayerPrefs.GetInt("Sound") == 0;

        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(AudioManager.SoundOn ? "Sprites/SoundOn" : "Sprites/SoundOff");
    }
}
