using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerDamagePotion : Projectile
{
    //protected float lifetime
    //protected Direction direction
    //protected float velocity

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction.x, direction.y) * velocity * Time.deltaTime;
    }
}
