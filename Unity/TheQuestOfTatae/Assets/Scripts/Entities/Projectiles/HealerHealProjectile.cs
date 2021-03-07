using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class HealerHealProjectile : Projectile
{
    //protected float lifetime
    //protected Direction direction
    //protected float velocity

    private void FixedUpdate()
    {
        Vector2 dir = direction.GetVector2();

        transform.position += new Vector3(dir.x, dir.y) * velocity * Time.deltaTime;
    }
}
