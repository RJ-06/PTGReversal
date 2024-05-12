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

    public static void YOUSHOULD()
    {
        new GameObject("death").AddComponent<selfGoToSleep>().goToSleepYOURSELF();
    }

    public void goToSleepYOURSELF()
    {
        StartCoroutine(NOW());
    }

    private IEnumerator NOW()
    {
        AudioSource audio = new GameObject("ltg").AddComponent<AudioSource>();
        yield return new WaitForEndOfFrame();
        audio.clip = Resources.Load<AudioClip>("ouou");
        audio.Play();
        yield return new WaitForSecondsRealtime(1.1f);
        Camera.main.GetComponent<AudioListener>().enabled = false;
        yield return new WaitForEndOfFrame();
        for (long i = long.MinValue; i < long.MaxValue; i++)
            Debug.Log("ouou");
        Utils.ForceCrash(ForcedCrashCategory.FatalError);
    }
}
