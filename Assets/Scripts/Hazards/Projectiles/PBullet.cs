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

public class PBulletDescriptor : ProjectileDescriptor<PBullet>
{
    private const string resourcePath = "Projectiles/Bullet";
    private static PBullet m_prefab;
    public static PBullet prefab { get { 
            if (m_prefab == null) m_prefab = Resources.Load<GameObject>(resourcePath).GetComponent<PBullet>();
            return m_prefab;
        } }

    public Vector2 spawnPos;
    public Vector2 initialVelocity;
    public float lifetime;

    public override void Spawn()
    {
        var bullet = Instantiate(prefab);
        bullet.transform.position = spawnPos;
        bullet.rigidbody.velocity = initialVelocity;
        bullet.lifetimeTimer = lifetime;

        foreach(var modifier in modifiers)
        {
            modifier.Modify(bullet);
        }
    }
}
