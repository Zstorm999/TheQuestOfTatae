using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField]
    protected float lifetime;

    public Direction direction { get; set; } = Direction.NONE;

    [SerializeField]
    protected float velocity;

    /// <summary>
    /// Destroys the projectile after its lifetime
    /// </summary>
    public void DestroyLater()
    {
        DestroyAfter(lifetime);
    }

    /// <summary>
    /// Destroys the projectile after the specified ammount of time
    /// </summary>
    /// <param name="time">The time in seconds to live. If negative, the object lives forever</param>
    public void DestroyAfter(float time)
    {
        if(time < 0f)
        {
            Debug.Log("Destroy time negative, object will live forever");
        }

        Destroy(transform.gameObject, time);

    }

}
