using UnityEngine;

public class HiddenTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/HiddenTile");

    private GameObject collider = null;
    protected override void OnBrickCollision()
    {
        return;
    }

    private void Awake()
    {
        Disable();
    }

    private void OnEnable()
    {
        Destroy(collider);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
        collider = Instantiate(ColliderTile.Tile, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
    }
}
