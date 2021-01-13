using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Brick")
            return;

        OnBrickCollision();
    }

    protected abstract void OnBrickCollision();
    
}
