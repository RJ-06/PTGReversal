using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GodotProject
{
    private const String GODOT_PATH_WINDOWS = "Assets/GodotBridge/Executables/Godot_v4.2.2-stable_win64.exe";
    private const String GODOT_PATH_LINUX = "Assets/GodotBridge/Executables/Godot_v4.2.2-stable_linux.x86_64";
    private const String GODOT_PATH_MAC = "Assets/GodotBridge/Executables/~macOS is my favorite operating system~/Contents/MacOS/Godot";
    private Process process;
    public GodotProject(String path)
    {
        String executablePath;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                executablePath = GODOT_PATH_WINDOWS;
                break;
            case RuntimePlatform.LinuxEditor:
                executablePath = GODOT_PATH_LINUX;
                break;
            case RuntimePlatform.OSXEditor:
                UnityEngine.Debug.LogWarning("hello poor mac user, please make sure this works correctly, good luck!");
                executablePath = GODOT_PATH_MAC;
                break;
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.LinuxPlayer:
            case RuntimePlatform.OSXPlayer:
                throw new Exception("TODO: make this work in exported projects");
            default:
                throw new Exception("what the *child friendly game* is your operating system?");
        }

        process = new Process();
        process.StartInfo.FileName = FileUtil.GetPhysicalPath(executablePath);
        process.StartInfo.ArgumentList.Add(path);
        process.StartInfo.RedirectStandardOutput = true;
    }

    public void Start()
    {
        process.Start();
    }
}
