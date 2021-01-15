using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class SoundButton : Button {

	void Start () 
    {
        AudioManager.Instance.SoundOn = PlayerPrefs.GetInt("Sound") == 1;
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(AudioManager.Instance.SoundOn ? "Sprites/SoundOn" : "Sprites/SoundOff");
    }

    protected override void OnClick()
    {
        AudioManager.Instance.Play(Sounds.Click);

        if (AudioManager.Instance.SoundOn = !AudioManager.Instance.SoundOn)
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
