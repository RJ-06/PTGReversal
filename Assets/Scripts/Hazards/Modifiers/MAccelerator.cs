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
