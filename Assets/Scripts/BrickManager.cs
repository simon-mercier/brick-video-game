using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [NotNull] private SwipeTouch swipeTouch;
    [NotNull] private KeyMovement keyMovement;

    public bool CanMove { get; set; } = false;
    public bool CanPlaySound { get; set; } = true;
    public bool IsUp { get; set; } = true;

    private Dir lastDir = Dir.NONE;
    private Dir dir = Dir.NONE;
    private bool swiped = false;

    private readonly float FALLEN_LEVEL = -25;

    void Start()
    {
        swipeTouch = gameObject.GetComponent<SwipeTouch>();
        keyMovement = gameObject.GetComponent<KeyMovement>();
    }

    void Update()
    {
        if (HasFallen())
            LevelManager.Instance.LevelFailed();

        if (!CanMove)
            return;

        DetectAccuratePosition();
        
        DetectSwipe();
        #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        DetectKeyPress();
        #endif
    }

    private bool HasFallen()
    {
        return transform.position.y <= FALLEN_LEVEL;
    }
    private void DetectAccuratePosition()
    {
        if (IsUp && swiped)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Round(transform.position.z));
            swiped = false;
        }
        else if ((lastDir == Dir.DOWN || lastDir == Dir.UP) && swiped)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, Mathf.Floor(transform.position.z) + 0.5f);
            swiped = false;
        }
        else if ((lastDir == Dir.LEFT || lastDir == Dir.RIGHT) && swiped)
        {
            transform.position = new Vector3(Mathf.Floor(transform.position.x) + 0.5f, transform.position.y, Mathf.Round(transform.position.z));
            swiped = false;
        }
        else if (swiped)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            swiped = false;
        }
    }

    private void DetectSwipe()
    {
        if (swipeTouch.SwipeDown) 
            AppropriateDir(Dir.DOWN);
        else if (swipeTouch.SwipeUp) 
            AppropriateDir(Dir.UP);
        else if (swipeTouch.SwipeLeft) 
            AppropriateDir(Dir.LEFT);
        else if (swipeTouch.SwipeRight) 
            AppropriateDir(Dir.RIGHT);
    }

    private void DetectKeyPress()
    {
        if (keyMovement.KeyDown) 
            AppropriateDir(Dir.DOWN);
        else if (keyMovement.KeyUp) 
            AppropriateDir(Dir.UP);
        else if (keyMovement.KeyLeft) 
            AppropriateDir(Dir.LEFT);
        else if (keyMovement.KeyRight) 
            AppropriateDir(Dir.RIGHT);
    }

    private void AppropriateDir(Dir direction)
    {
        AudioManager.Instance.Play(Sounds.Move);

        dir = direction;
        CanPlaySound = true;
        swiped = true;
        var moveByLR = 1.02f;
        var moveByUD = 1f;
        var anim = GetComponent<Animator>();
        anim.SetTrigger("doDefault");
        switch (direction)
        {
            case Dir.DOWN:

                if (IsUp)
                {
                    IsUp = false;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    moveByUD = 0;
                    transform.Rotate(new Vector3(-90, 0, 0));
                }
                else if (lastDir == Dir.DOWN || lastDir == Dir.UP)
                {
                    IsUp = true;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    transform.Rotate(new Vector3(-90, 0, 0));

                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 90, 0));
                }
                transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveByUD, gameObject.transform.position.z - moveByLR); //DOWN

                break;
            case Dir.UP:

                if (IsUp)
                {
                    IsUp = false;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    moveByUD = 0;
                    transform.Rotate(new Vector3(90, 0, 0));
                }
                else if (lastDir == Dir.DOWN || lastDir == Dir.UP)
                {
                    IsUp = true;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    transform.Rotate(new Vector3(90, 0, 0));

                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 90, 0));
                }
                transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveByUD, gameObject.transform.position.z + moveByLR); //UP

                break;
            case Dir.LEFT:

                if (IsUp)
                {
                    IsUp = false;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    moveByUD = 0;
                    transform.Rotate(new Vector3(0, 0, 90));
                }
                else if (lastDir == Dir.LEFT || lastDir == Dir.RIGHT)
                {
                    IsUp = true;
                    lastDir = direction;
                    moveByLR = 1.5f;
                    transform.Rotate(new Vector3(0, 0, 90));
                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 90, 0));
                }
                transform.position = new Vector3(gameObject.transform.position.x - moveByLR, gameObject.transform.position.y + moveByUD, gameObject.transform.position.z); //LEFT

                break;
            case Dir.RIGHT:

                if (IsUp)
                {
                    IsUp = false;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    moveByUD = 0;
                    transform.Rotate(new Vector3(0, 0, -90));
                }
                else if (lastDir == Dir.LEFT || lastDir == Dir.RIGHT)
                {
                    IsUp = true;
                    lastDir = direction;
                    moveByLR = 1.52f;
                    transform.Rotate(new Vector3(0, 0, -90));
                }
                else
                {
                    transform.GetChild(0).Rotate(new Vector3(0, 90, 0));
                }
                transform.position = new Vector3(gameObject.transform.position.x + moveByLR, gameObject.transform.position.y + moveByUD, gameObject.transform.position.z); //RIGHT
                break;
            case Dir.NONE:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}
public enum Dir
{
    UP, DOWN, LEFT, RIGHT, NONE
}

