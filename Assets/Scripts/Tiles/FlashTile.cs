using UnityEngine;

public class FlashTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/FlashTile");

    protected override void OnBrickCollision()
    {
        if (!BrickManager.IsUp)
            return;

    }
}
