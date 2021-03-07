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
        transform.position += new Vector3(direction.x, direction.y) * velocity * Time.deltaTime;
    }
}
