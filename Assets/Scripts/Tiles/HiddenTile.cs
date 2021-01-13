using UnityEngine;

public class HiddenTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/HiddenTile");

    protected override void OnBrickCollision()
    {
        if (!BrickManager.IsUp)
            return;

    }
}
