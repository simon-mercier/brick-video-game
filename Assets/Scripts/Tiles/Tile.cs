using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Brick")
            return;

        OnBrickCollision();
    }
    protected abstract void OnBrickCollision();
    
}
