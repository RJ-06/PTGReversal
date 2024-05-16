using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSineOscillator : MonoBehaviour
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

public class MSineOscDescriptor : ProjectileModifier<PBullet>
{
    public override void Modify(PBullet projectile)
    {
        throw new System.NotImplementedException();
    }
}
