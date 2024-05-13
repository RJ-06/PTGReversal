using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] float knockbackStrength;
    [SerializeField] float maxSpeed;
    [SerializeField] float dashSpeedMultiplier;
    [SerializeField] float dashTurnMultiplier;
    [SerializeField] float dashDuration;
    [SerializeField] float dashFalloffDuration;
    [SerializeField] float dashCooldown;
    [SerializeField] float turnSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hp = mh;
    }

    private void FixedUpdate()
    {
        if (td > 0)
            td -= dt;
        if (cd > 0)
            cd -= dt;
        if (ms != 0)
        {
            if(td > 0)
            {
                md = rt(md, mi, ts * dt * lp(1, dr, td / df));
            }
            else if(mn(mi) != 0 && dp(nm(mi), md) > -0.5f)
            {
                md = rt(md, mi, ts * dt);
            }
            else
            {
                var di = 2 + dp(nm(mi), md) * 2;
                md = rt(md, mi, ts * di * di * dt);
            }
        }
        else if(mn(mi) != 0)
        {
            md = nm(mi);
        }

        if (td <= 0)
        {
            var ss = mn(mi) * mx;
            if (dp(mi, md) < 0)
            {
                ss *= -1;
            }
            var sd = ss - ms;
            if (sd > 0)
            {
                var aq = ac * dt;
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
            else if (sd < 0)
            {
                var dq = -dc * dt;
                //Debug.Log($"D {sd}, {ms}, {dq}");
                if (dq <= sd)
                {
                    ms = ss;
                }
                else
                {
                    ms += dq;
                    if (ms < 0)
                    {
                        ms *= -1;
                        md *= -1;
                    }
                }
            }
        }
        transform.position += dt * ms * cv(md);
    }

    private void OnTriggerEnter2D(Collider2D cl)
    {
        if(cl.CompareTag("Damager"))
        {
            hp--;
            var kb = nm(transform.position - cl.transform.position) * ks;
            SetVelocity(kb);
            if(hp < 0)
            {
                enabled = false;
                selfGoToSleep.YOUSHOULD();
            }
        }
    }

    public void SetVelocity(Vector2 vl)
    {
        ms = mn(vl);
        md = nm(vl);
    }

    private void OnMove(InputValue vl)
    {
        mi = vl.Get<Vector2>();
        playClip();

    }

    public void playClip()
    {
        au.clip = ad;
        au.Play();
    }

    private void OnLook(InputValue vl)
    {
        var va = vl.Get<Vector2>();
        if(mn(va) != 0)
            li = nm(va);
    }

    private void OnFire()
    {
        if (mn(mi) == 0 || cd > 0)
            return;
        SetVelocity(ds * mx * nm(mi));
        td = dd + df;
        cd = dw;
    }

    #region Variables
    private int mh => maxHealth;
    private float ks => knockbackStrength;
    private float mx => maxSpeed;
    private float ds => dashSpeedMultiplier;
    private float dr => dashTurnMultiplier;
    private float dd => dashDuration;
    private float df => dashFalloffDuration;
    private float dw => dashCooldown;
    private float ts => turnSpeed;
    private float ac => acceleration;
    private float dc => deceleration;
    private float dt => Time.fixedDeltaTime;
    private int hp;
    private float ms;
    private Vector2 md;
    private Vector2 mi;
    private float td;
    private float cd;
    private Vector2 li;
    private Rigidbody2D rb;
    private AudioSource au => audioSource;
    private AudioClip ad => audioClip;
    #endregion
    #region Helper Functions
    private Vector2 nm(Vector2 v) { return v.normalized; }
    private float mn(Vector2 v) { return v.magnitude; }
    private Vector2 rt(Vector2 v1, Vector2 v2, float a) { return Vector3.RotateTowards(v1, v2, a, 0).normalized; }
    private float an(Vector2 v1, Vector2 v2) { return Vector2.Angle(v1, v2); }
    private Vector3 cv(Vector2 v) { return v; }
    private float dp(Vector2 v1, Vector2 v2) { return Vector2.Dot(v1, v2); }
    private float lp(float f1, float f2, float t) { return Mathf.Lerp(f1, f2, t); }
    #endregion
}
