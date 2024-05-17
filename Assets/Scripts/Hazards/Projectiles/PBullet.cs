using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBullet : ProjectileBase
{
    [HideInInspector] public float lifetimeTimer;
    public new Rigidbody2D rigidbody;
    public new Collider2D collider;
    public new SpriteRenderer renderer;

    private void FixedUpdate()
    {
        lifetimeTimer -= Time.fixedDeltaTime;
        if (lifetimeTimer <= 0)
            Destroy(gameObject);
    }
}
