using UnityEngine;

public class CheckPointTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/CheckPointTile");

    protected override void OnBrickCollision()
    {
        if (!LevelManager.Instance.Brick.GetComponent<BrickManager>().IsUp)
            return;

        LevelManager.Instance.ActivateHiddenCheckPoint();
    }
}
