using UnityEngine;

public class CkeckPointTempTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/CheckPointTempTile");

    protected override void OnBrickCollision()
    {
        if (!LevelManager.Instance.Brick.GetComponent<BrickManager>().IsUp)
            return;

        LevelManager.Instance.ActivateHiddenTempCheckPoint();
    }
}
