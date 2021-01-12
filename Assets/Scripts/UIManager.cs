using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance ??= new UIManager();
    public GameObject UIGameObject => Instance.gameObject;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
