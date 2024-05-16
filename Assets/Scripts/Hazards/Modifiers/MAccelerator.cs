using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAccelerator : MonoBehaviour
{
    public Vector2 relativeAccel;
    public Vector2 absoluteAccel;

    public new Rigidbody2D rigidbody;

    private void FixedUpdate()
    {
        Vector2 velDirection = rigidbody.velocity.normalized;
        Vector2 acceleration = relativeAccel.x * velDirection + relativeAccel.y * new Vector2(velDirection.y, -velDirection.x) + absoluteAccel;
        rigidbody.velocity += acceleration * Time.fixedDeltaTime;
    }
}

public class MAcceleratorDescriptor : ProjectileModifier<PBullet>
{
    public float tanAccel;
    public float centAccel;
    public Vector2 constAccel;

    public override void Modify(PBullet projectile)
    {
        MAccelerator accel = projectile.gameObject.AddComponent<MAccelerator>();
        accel.relativeAccel = new Vector2(tanAccel, centAccel);
        accel.absoluteAccel = constAccel;
        accel.rigidbody = projectile.rigidbody;
    }
}