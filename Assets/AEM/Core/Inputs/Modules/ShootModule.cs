using System.Collections.Generic;
using UnityEngine;

public class ShootModule : AEMModule
{
    public bool shoot = false;
    public float Force = 1f;

    public GameObject Projectile;
    List<GameObject> projectileList;

    void Start()
    {
        if (!Projectile)
            Debug.LogWarning(this + "projectile not assign");

        projectileList = new List<GameObject>();
    }

    void Update()
    {
        ShootModuleLogic();
    }

    protected virtual void ShootModuleLogic()
    {
        if (shoot)
            if (Projectile)
            {
                //Spawn projectile
                GameObject spawnedGO = Instantiate(Projectile, transform.position, transform.rotation);
                spawnedGO.SetActive(true);

                //Add to list for tracking purposes
                projectileList.Add(spawnedGO);

                //Apply forward force
                Rigidbody r = spawnedGO.GetComponent<Rigidbody>();
                if (r)
                {
                    r.AddForce(transform.forward * Force,ForceMode.Impulse);
                }
            }
                
    }
}