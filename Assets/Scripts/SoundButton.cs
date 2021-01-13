using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundButton : MonoBehaviour {

	void Start () 
    {
        AudioManager.SoundOn = PlayerPrefs.GetInt("Sound") == 1;

        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(AudioManager.SoundOn ? "Sprites/SoundOn" : "Sprites/SoundOff");
    }
    public void OnClick_Sound()
    {
        AudioManager.Play(Sounds.Click);

        AudioManager.SoundOn = !AudioManager.SoundOn;

        if (AudioManager.SoundOn)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SoundOn");
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/SoundOff");
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
}
