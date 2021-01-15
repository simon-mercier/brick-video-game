using UnityEngine;

public class NormalTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/NormalTile");

    protected override void OnBrickCollision()
    {
            return;
    }
}
