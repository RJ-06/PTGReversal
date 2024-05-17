using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accelerator", menuName = "Hazards/Modifiers/Bullet/Accelerator")]
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
