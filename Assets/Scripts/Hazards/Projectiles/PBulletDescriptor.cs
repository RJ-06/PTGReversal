using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Hazards/Projectiles/Bullet")]
public class PBulletDescriptor : ProjectileDescriptor<PBullet>
{
    private const string resourcePath = "Projectiles/Bullet";
    private static PBullet m_prefab;
    public static PBullet prefab
    {
        get
        {
            if (m_prefab == null) m_prefab = Resources.Load<GameObject>(resourcePath).GetComponent<PBullet>();
            return m_prefab;
        }
    }

    public Vector2 spawnPos;
    public Vector2 initialVelocity;
    public float lifetime = 5;

    public override void Spawn()
    {
        PBullet bullet = Instantiate(prefab);
        bullet.transform.position = spawnPos;
        bullet.rigidbody.velocity = initialVelocity;
        bullet.lifetimeTimer = lifetime;

        foreach (var modifier in modifiers)
        {
            modifier.Modify(bullet);
        }
    }
}
