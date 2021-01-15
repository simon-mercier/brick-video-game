using UnityEngine;

public class HiddenTempTile : Tile
{
    public static GameObject Tile => Resources.Load<GameObject>("Prefabs/Tiles/HiddenTempTile");

    private static readonly float TIME_UNTIL_DISABLED = 10f;

    private GameObject collider = null;

    protected override void OnBrickCollision()
    {
        return;
    }

    private void Awake()
    {
        collider = Instantiate(ColliderTile.Tile, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent);
        Disable();
    }

    private void OnEnable()
    {
        collider.SetActive(false);

        if (IsInvoking("Disable"))
            CancelInvoke("Disable");

        Invoke("Disable", TIME_UNTIL_DISABLED);
    }

    private void Disable()
    {
        this.gameObject.SetActive(false);
        collider.SetActive(true);
    }
}
