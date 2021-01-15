using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/EndTile");

    protected override void OnBrickCollision()
    {
        if (!LevelManager.Instance.Brick.GetComponent<BrickManager>().IsUp)
            return;

        LevelManager.Instance.LevelCompleted();
    }
}

