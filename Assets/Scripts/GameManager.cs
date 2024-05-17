using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{

    void Awake()
    {
        banana = GetComponent<PlayableDirector>();
        banana.timeUpdateMode = DirectorUpdateMode.Manual;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadMinus) && banana.state == PlayState.Paused)
        {
            banana.Play();
            banana.time = 0;
        }
    }

    void FixedUpdate()
    {
        if(banana.state == PlayState.Playing)
        {
            banana.Evaluate();
            banana.time += Time.fixedDeltaTime;
        }
    }



    #region Variables
    private PlayableDirector banana;
    #endregion
}
