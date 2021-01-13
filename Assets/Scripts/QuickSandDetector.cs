using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSandDetector : MonoBehaviour
{
    public bool IsQs { get; set; } = false;
    public static bool AlreadyTriggered { get; set; } = false;

    private static MakeMap _MakeMap;


    private void OnTriggerEnter(Collider other)
    {
        if(IsQs && other.name == "Brick(Clone)")
        {
            Invoke("QSTilesFall", 1f);
            StartCoroutine(SetNotActive(1f));
        }
        else if (!AlreadyTriggered && other.name == "Brick(Clone)")
        {
            AlreadyTriggered = true;
            BrickManager.CanMove = false;
            StartCoroutine(KillBrick(.6f, other));
        }
    }

    private IEnumerator KillBrick(float timeDelay, Collider other)
    {
        yield return new WaitForSeconds(timeDelay);
        MakeMap.RestartLevel();
    }

    private IEnumerator SetNotActive(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        gameObject.SetActive(false);
    }

    private void QsTilesFall()
    {
        _MakeMap = FindObjectOfType<MakeMap>();
        MakeMap.SetTilesKinematicState(false, _MakeMap.QuickSandTiles, _MakeMap.QuickSandTilesReference.IndexOf(transform.position));
    }
}
