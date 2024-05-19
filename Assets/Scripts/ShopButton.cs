using System.Collections;
using System.Collections.Generic;
using GodotBridge;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    GodotProject project;
    private bool firstTime = true;
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
        project.Start($"money={gameManager.money} first-time={firstTime}");
        project.MessageRecieved += OnMessageRecieved;
        firstTime = false;
    }

    private void OnMessageRecieved(object sender, string message) 
    {
        Debug.Log(message);
        string key = message.Split("=")[0];
        string value = message.Split("=")[1];
        switch (key)
        {
            case "money":
                gameManager.money = int.Parse(value);
                break;
            case "speed":
                // TODO
                break;
            case "health":
                // TODO
                break;
        }
    }
}
