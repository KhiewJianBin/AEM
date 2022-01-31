using System.Linq;
using UnityEngine;
public class CollidableObject : AEMObject
{
    protected override void OnEnable()
    {
        //Check for any collision module and subscribe it to Collision event
        CollisionModule collision = GetComponent<CollisionModule>();
        if (collision)
            collision.OnObjectCollision += CollisionHitEvent;
    }

    protected override void OnDisable()
    {
        CollisionModule collision = GetComponent<CollisionModule>();
        if (collision)
            collision.OnObjectCollision -= CollisionHitEvent;
    }

    public virtual void Update()
    {
        ModuleList = GetComponentsInChildren<AEMModule>().ToList();
        BuffModuleList.RemoveAll(item => item == null);

        foreach (AEMModule module in ModuleList)
        {

        }
    }
    
    void CollisionHitEvent(Collision collidedobj)
    {
        HealthModule healthmodule = GetComponent<HealthModule>();
        if (healthmodule)
        {
            ProjectileController collidedprojectile = collidedobj.gameObject.GetComponent<ProjectileController>();
            if (collidedprojectile)
            {
                //healthmodule.Damage(collidedprojectile.GetEffectiveDamage());
            }
        }
    }
}
