using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class selfGoToSleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goToSleepYOURSELF()
    {
        Utils.ForceCrash(ForcedCrashCategory.FatalError);
    }
}
