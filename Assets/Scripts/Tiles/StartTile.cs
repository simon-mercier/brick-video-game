using UnityEngine;

public class StartTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/StartTile");

    protected override void OnBrickCollision()
    {
        if (!LevelManager.Instance.Brick.GetComponent<BrickManager>().IsUp)
            return;

        LevelManager.Instance.Brick.GetComponent<BrickManager>().CanMove = true;
    }
}
