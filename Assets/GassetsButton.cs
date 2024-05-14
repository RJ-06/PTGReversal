using GodotBridge;
using UnityEngine;
using UnityEditor;

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
    
    #if UNITY_EDITOR
    [MenuItem("GassetsMenu/Open Godot Editor _g")]
    static public void RunEditor()
    {
        GodotProject project2 = new("Gassets", "scenes/menu.tscn");
        project2.StartEditor();
        GodotProject project3 = new("pass-the-godot", "main.tscn");
        project3.StartEditor();
    }
    #else
    static public void RunEditor() { /* stub */ }
    #endif
}
