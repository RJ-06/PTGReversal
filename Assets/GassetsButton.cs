using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GassetsButton : MonoBehaviour
{
    GodotProject project;
    // Start is called before the first frame update
    void Start()
    {
        project = new GodotProject("Gassets", "scenes/menu.tscn");
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
