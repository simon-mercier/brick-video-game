using UnityEngine;

public class SandTile : Tile
{


    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/SandTile");

    protected override void OnBrickCollision()
    {

    }
}
