using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour
{

    private bool keyPress;

    float timer = 0.0f;
    float timeDone = 0.2f;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeDone)
        {
            keyPress = false;
            timer = 0.0f;
        }
        KeyUp = KeyDown = KeyLeft = KeyRight = false;
        if (Input.GetKeyDown(KeyCode.W) && !keyPress) KeyUp = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.S) && !keyPress) KeyDown = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.A) && !keyPress) KeyLeft = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.D) && !keyPress) KeyRight = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !keyPress) KeyDown = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && !keyPress) KeyLeft = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !keyPress) KeyRight = keyPress = true;
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !keyPress) KeyUp = keyPress = true;
    }

    public bool KeyLeft { get; private set; }
    public bool KeyRight { get; private set; }
    public bool KeyUp { get; private set; }
    public bool KeyDown { get; private set; }
}
