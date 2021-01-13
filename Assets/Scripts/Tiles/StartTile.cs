using UnityEngine;

public class StartTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/StartTile");

    protected override void OnBrickCollision()
    {
        if (!BrickManager.IsUp)
            return;

    }
}
