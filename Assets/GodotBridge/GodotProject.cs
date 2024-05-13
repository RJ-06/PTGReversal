using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using UnityEngine;

public class GodotProject
{
    private const string GODOT_ZIP_WINDOWS = "GodotBinaries/Godot_v4.2.2-stable_win64.exe.zip";
    private const string GODOT_ZIP_LINUX = "GodotBinaries/Godot_v4.2.2-stable_linux.x86_64.zip";
    private const string GODOT_ZIP_MAC = "GodotBinaries/Godot_v4.2.2-stable_macos.universal.zip";
    private const string GODOT_BINARY_WINDOWS = "Godot_v4.2.2-stable_win64.exe";
    private const string GODOT_BINARY_LINUX = "Godot_v4.2.2-stable_linux.x86_64";
    private const string GODOT_BINARY_MAC = "Godot.app/Contents/MacOS/Godot";
    private const string MESSAGE_TOKEN = "[Unity Bridge]: ";
    private readonly Process process;
    private readonly string engineArguments;
    private readonly string engineArgumentsEditor;

    public event EventHandler<String> MessageRecieved;
    public GodotProject(string projectPath, string scene)
    {
        string zipPath;
        string binaryPath;

        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
            case RuntimePlatform.WindowsPlayer:
                zipPath = GODOT_ZIP_WINDOWS;
                binaryPath = GODOT_BINARY_WINDOWS;
                break;

            case RuntimePlatform.LinuxEditor:
            case RuntimePlatform.LinuxPlayer:
                zipPath = GODOT_ZIP_LINUX;
                binaryPath = GODOT_BINARY_LINUX;
                break;

            case RuntimePlatform.OSXEditor:
            case RuntimePlatform.OSXPlayer:
                UnityEngine.Debug.LogWarning("i don't have a mac so i'm doubting this will work");
                zipPath = GODOT_ZIP_MAC;
                binaryPath = GODOT_BINARY_MAC;
                break;

            default:
                throw new Exception("please use a normal person operating system");
        }

        zipPath = Path.Combine(Application.streamingAssetsPath, zipPath);
        binaryPath = Path.Combine(Application.persistentDataPath, binaryPath);

        if (!File.Exists(binaryPath))
        {
            UnityEngine.Debug.Log("extracted binary to " + binaryPath);
            ZipFile.ExtractToDirectory(zipPath, Application.persistentDataPath, true);
        }

        // ATTENTION MAC USERS: You might need to do something similar to this to get this to work on mac
        if (Application.platform is RuntimePlatform.LinuxEditor or RuntimePlatform.LinuxPlayer) 
        {
            // mark the godot binary as executable (linux)
            Process.Start("chmod", "a+x " + binaryPath);
        }

        process = new Process();
        process.StartInfo.FileName = binaryPath;
        engineArguments = "-v --path ";
        engineArguments += Path.Combine(Application.streamingAssetsPath, projectPath) + " ";
        engineArguments += scene + " ";

        engineArgumentsEditor = Path.Combine(Application.streamingAssetsPath, projectPath, "project.godot");

        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;

        process.OutputDataReceived += OnOutputDataRecieved;
    }

    public void Start(string userArguments)
    {
        process.StartInfo.Arguments = engineArguments + " ++ " + userArguments;
        process.Start();
    }

    public void Start()
    {
        Start("");
    }

    public void StartEditor()
    {
        process.StartInfo.Arguments = engineArgumentsEditor;
        process.Start();
    }
    private void OnOutputDataRecieved(object sender, DataReceivedEventArgs e)
    {
        UnityEngine.Debug.Log(e.Data);
        if (e.Data.StartsWith(MESSAGE_TOKEN))
        {
            MessageRecieved.Invoke(this, e.Data[MESSAGE_TOKEN.Length..]);
        }
    }
}
