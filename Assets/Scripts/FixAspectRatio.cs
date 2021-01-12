using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAspectRatio : MonoBehaviour {

    private void Update()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector2(gameObject.GetComponent<RectTransform>().localPosition.x,(2375/3f)/ (float)(Screen.width / (float)Screen.height));
    }

    
}
