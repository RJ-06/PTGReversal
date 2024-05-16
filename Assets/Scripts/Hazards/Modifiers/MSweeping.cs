using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSweeping : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MSweepDescriptor : ProjectileModifier<PBeam>
{
    public override void Modify(PBeam projectile)
    {
        throw new System.NotImplementedException();
    }
}
