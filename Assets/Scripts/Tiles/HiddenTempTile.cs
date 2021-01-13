using UnityEngine;

public class HiddenTempTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/HiddenTempTile");


    protected override void OnBrickCollision()
    {
        if (!BrickManager.IsUp)
            return;

    }
}
