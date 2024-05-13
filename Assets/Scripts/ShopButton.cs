using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ShopButton : MonoBehaviour
{
    GodotProject project;
    // Start is called before the first frame update
    void Start()
    {
        project = new GodotProject("pass-the-godot", "main.tscn");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        project.Start();
    }
}
