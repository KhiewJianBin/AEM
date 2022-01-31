using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float Damage = 1f;

    public float GetEffectiveDamage()
    {
        //apply modifiers

        return Damage;
    }
}
