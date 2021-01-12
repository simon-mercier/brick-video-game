using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothScroll : MonoBehaviour {
    private float fResistanceFactor = 0.98f;
    private float fStopThreashold = 0.01f;

    private Plane planeHit;
    private Vector3 v3StartPos;
    private Vector3 v3LastPos;
    private Vector3 v3Delta;
    private float fStartTime;

    private bool bTranslating = false;

    void OnMouseDown()
    {
        bTranslating = false;
        v3StartPos = GetHitPoint();
        v3LastPos = v3StartPos;
        fStartTime = Time.time;
    }

    void OnMouseDrag()
    {
        Vector3 v3T = GetHitPoint();
        transform.Translate(v3T - v3LastPos);
        v3LastPos = v3T;
    }

    void OnMouseUp()
    {
        v3Delta = GetHitPoint();
        v3Delta = (v3Delta - v3StartPos) / (Time.time - fStartTime) * Time.deltaTime;
        bTranslating = true;
    }

    Vector3 GetHitPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float fDist;
        if (planeHit.Raycast(ray, out fDist))
            return ray.GetPoint(fDist);
        else
            return Vector3.zero;
    }

    void Start()
    {
        planeHit = new Plane(Vector3.forward, transform.position);
    }

    void Update()
    {
        if (bTranslating)
        {
            transform.position += v3Delta;
            v3Delta = v3Delta * fResistanceFactor;
            if (v3Delta.magnitude < fStopThreashold)
                bTranslating = false;
        }
    }
}
