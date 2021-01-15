using UnityEngine;
using UnityEngine.SceneManagement;

public class ColliderTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/ColliderTile");

    protected override void OnBrickCollision()
    {
        LevelManager.Instance.LevelFailed();
    }
}