using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTouch : MonoBehaviour {

    private bool tap, isDraging;
    private Vector2 startTouch;


    // Update is called once per frame
    void Update()
    {
        tap = SwipeLeft = SwipeRight = SwipeUp = SwipeDown = false;
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {

            Reset();
        }
        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {

                tap = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion
        Swipe();
    }
    private void DeadZone(int rayon)
    {
        if (!(SwipeDelta.magnitude > rayon)) return;
        var x = SwipeDelta.x;
        var y = SwipeDelta.y;
        IsUp = !IsUp;
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x < 0)
                SwipeLeft = true; 
            else
                SwipeRight = true;
        }
        else
        {
            if (y < 0)
                SwipeDown = true;
            else
                SwipeUp = true;
        }
        Reset();
    }

    private void Swipe()
    {
        CalculateDistance();
        DeadZone(35);
    }

    private void Scroll()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + SwipeDelta.x, gameObject.transform.position.y);
    }

    private void CalculateDistance()
    {
        SwipeDelta = Vector2.zero;
        if (!isDraging) return;
        if (Input.touches.Length > 0)
            SwipeDelta = Input.touches[0].position - startTouch;
        else if(Input.GetMouseButton(0))
            SwipeDelta = (Vector2)Input.mousePosition - startTouch;
    }
    private void Reset()
    {
        startTouch = SwipeDelta = Vector2.zero;
        isDraging = false;
    }

    public Vector2 SwipeDelta { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }
    public bool IsUp { get; private set; }
}
