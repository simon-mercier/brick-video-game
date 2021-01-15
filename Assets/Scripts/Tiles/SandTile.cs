using UnityEngine;

public class SandTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/SandTile");

    private static readonly float TIME_UNTIL_FALL = 1f;
    private static readonly float TIME_UNTIL_DESTROYED = 3f;
    protected override void OnBrickCollision()
    {
        Invoke("SandFall", TIME_UNTIL_FALL);
        Destroy(gameObject, TIME_UNTIL_DESTROYED);
    }

    private void SandFall()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Instantiate(ColliderTile.Tile, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
    }
}
