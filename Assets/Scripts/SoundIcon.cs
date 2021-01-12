using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundIcon : MonoBehaviour {

	void Start () 
    {
        AudioManager.SoundOn = PlayerPrefs.GetInt("Sound") == 0;

        gameObject.GetComponent<Image>().sprite = AudioManager.SoundOn ? Resources.Load<Sprite>("Sprites/SoundOn") : Resources.Load<Sprite>("Sprites/SoundOff");

        
    }
}
