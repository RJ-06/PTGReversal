using System.Collections;
using System.Collections.Generic;
using GodotBridge;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ShopButton : MonoBehaviour
{
    GodotProject project;
    // Start is called before the first frame update
    void Start()
    {
        project = new GodotProject("pass-the-godot", "scenes/main.tscn");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        project.Start();
        project.MessageRecieved += OnPlayerJumpInGodot;
    }

    private void OnPlayerJumpInGodot(object sender, string e) 
    {
        Debug.Log(e);
        if (e == "jump")
        {
            Debug.Log("Player jumped in Godot!");
        }
    }
}
