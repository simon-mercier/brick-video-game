using UnityEngine;

public class FlashTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/FlashTile");

    private static readonly float TIME_UNTIL_DISABLED = 3f;

    private GameObject collider = null;
    protected override void OnBrickCollision()
    {
        return;
    }

    private void Awake()
    {
        collider = Instantiate(ColliderTile.Tile, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        collider.SetActive(false);

        InvokeRepeating("ToggleActiveFlashTile", TIME_UNTIL_DISABLED, TIME_UNTIL_DISABLED);
    }

    private void ToggleActiveFlashTile()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        collider.SetActive(!collider.activeSelf);
    }

}
