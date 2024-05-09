using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float turnSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (ms != 0)
        {
            if(mi.magnitude != 0 && Vector2.Dot(mi.normalized, md) > -0.5f)
            {
                md = Vector3.RotateTowards(md, mi, ts * dt, 0);
            }
            else
            {
                float di = 2 + Vector2.Dot(mi.normalized, md) * 2;
                md = Vector3.RotateTowards(md, mi, ts * di * di * di * dt, 0);
            }
        }
        else if(mi.magnitude != 0)
        {
            md = mi.normalized;
        }

        float ss = mi.magnitude * mx;
        if (Vector2.Dot(mi, md) < 0)
        {
            ss *= -1;
        }
        float sd = ss - ms;
        if(sd > 0)
        {
            float aq = ac * dt;
            //Debug.Log($"A {sd}, {ms}, {aq}");
            if (aq >= sd)
            {
                ms = ss;
            }
            else
            {
                ms += aq;
            }
        }
        else if(sd < 0)
        {
            float dq = -dc * dt;
            //Debug.Log($"D {sd}, {ms}, {dq}");
            if (dq <= sd)
            {
                ms = ss;
            }
            else
            {
                ms += dq;
                if(ms < 0)
                {
                    ms *= -1;
                    md *= -1;
                }
            }
        }

        transform.position += (Vector3)md * ms * dt;
    }

    private void OnMove(InputValue value)
    {
        mi = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        li = value.Get<Vector2>();
    }



    #region Variables
    private float mx => maxSpeed;
    private float ts => turnSpeed;
    private float ac => acceleration;
    private float dc => deceleration;
    private float dt => Time.fixedDeltaTime;
    private float ms;
    private Vector2 md;
    private Vector2 mi;
    private Vector2 li;
    private Rigidbody2D rb;
    #endregion
}