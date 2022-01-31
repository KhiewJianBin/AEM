using UnityEngine;

public class CollisionModule : AEMModule
{
    //Module that enables an object to detect for collision

    public delegate void ObjectCollisionHandler(Collision collidedobj);
    public event ObjectCollisionHandler OnObjectCollision;

    void Awake()
    {
        if (GetComponent<CollisionModule>() != this)
        {
            Debug.Log("Objects can only have one Collision Module. Removing "+this+".");
            Destroy(this);
        }
    }

    protected virtual void OnCollisionEnter(Collision collidedobj)
    {
        if (OnObjectCollision != null)
        {
            OnObjectCollision(collidedobj);
        }
    }
}