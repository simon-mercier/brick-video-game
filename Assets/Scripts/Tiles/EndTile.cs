using UnityEngine;

public class EndTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/EndTile");

    protected override void OnBrickCollision()
    {
        if (!BrickManager.IsUp)
            return;

    }
}

