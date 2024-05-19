using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GodotBridge
{
    public class GodotProject
    {
        private const string GodotZipWindows = "GodotBinaries/Godot_win64.zip";
        private const string GodotZipLinux = "GodotBinaries/Godot_v4.2.2-stable_linux.x86_64.zip";
        private const string GodotZipMac = "GodotBinaries/Godot_v4.2.2-stable_macos.universal.zip";
        private const string GodotBinaryWindows = "Godot.exe";
        private const string GodotBinaryLinux = "Godot_v4.2.2-stable_linux.x86_64";
        private const string GodotBinaryMac = "Godot.app/Contents/MacOS/Godot";
        private const string MessageToken = "[Unity Bridge]: ";
        private readonly Process _process;
        private readonly string _engineArguments;
        private readonly string _engineArgumentsEditor;

        public event EventHandler<String> MessageRecieved;

        public GodotProject(string projectPath, string scene)
        {
            string zipPath;
            string binaryPath;

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    zipPath = GodotZipWindows;
                    binaryPath = GodotBinaryWindows;
                    break;

                case RuntimePlatform.LinuxEditor:
                case RuntimePlatform.LinuxPlayer:
                    zipPath = GodotZipLinux;
                    binaryPath = GodotBinaryLinux;
                    break;

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    Debug.LogWarning("i don't have a mac so i'm doubting this will work");
                    zipPath = GodotZipMac;
                    binaryPath = GodotBinaryMac;
                    break;

                default:
                    throw new Exception("please use a normal person operating system");
            }

            var temporaryProvider = Path.GetTempPath();
            // Try the environment variable first
            if (Environment.GetEnvironmentVariable("godot_bin") is { } tmp)
            {
                temporaryProvider = tmp;
            }

            const string leaf = "PTGReverseChaos";
            temporaryProvider = Path.Combine(temporaryProvider, leaf);
            // make sure the directory exists
            Directory.CreateDirectory(temporaryProvider);
            Debug.Log("Godot binaries are in: " + temporaryProvider);

            zipPath = Path.Combine(Application.streamingAssetsPath, zipPath);
            binaryPath = Path.Combine(temporaryProvider, binaryPath);


            if (!File.Exists(binaryPath))
            {
                Debug.Log("extracted binary to " + binaryPath);
                // make a temporary directory
                ZipFile.ExtractToDirectory(zipPath, temporaryProvider, true);
            }

            // ATTENTION MAC USERS: You might need to do something similar to this to get this to work on mac
            if (Application.platform is RuntimePlatform.LinuxEditor or RuntimePlatform.LinuxPlayer)
            {
                // mark the godot binary as executable (linux)
                Process.Start("chmod", "a+x " + binaryPath);
            }

            _process = new Process();
            _process.StartInfo.FileName = binaryPath;
            _engineArguments = "--path ";
            _engineArguments += Path.Combine(Application.streamingAssetsPath, projectPath) + " ";
            _engineArguments += scene + " ";

            _engineArgumentsEditor = Path.Combine(Application.streamingAssetsPath, projectPath, "project.godot");

            _process.StartInfo.RedirectStandardOutput = true;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.UseShellExecute = false;

            _process.OutputDataReceived += OnOutputDataRecieved;
            _process.ErrorDataReceived += OnErrorDataRecieved;

            _process.EnableRaisingEvents = true;
            _process.Exited += OnProcessExited;
            Debug.Log("GodotProject init completed");
        }
        public void Start(string userArguments)
        {
            _process.StartInfo.Arguments = _engineArguments + " ++ " + userArguments;
            Debug.Log("Start: " + _process.StartInfo.FileName + " with " + _process.StartInfo.Arguments);
            _process.Start();

            _process.BeginOutputReadLine();
            _process.BeginErrorReadLine();
            
        }

        public void Start()
        {
            Start("");
        }

        public void StartEditor()
        {
            _process.StartInfo.Arguments = _engineArgumentsEditor;
            _process.Start();
        }

        private void OnOutputDataRecieved(object sender, DataReceivedEventArgs e)
        {
            if (e.Data.StartsWith(MessageToken))
            {
                MessageRecieved?.Invoke(this, e.Data[MessageToken.Length..]);
            }
        }

        private void OnErrorDataRecieved(object sender, DataReceivedEventArgs e)
        {
            Debug.Log(e.Data);
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            Debug.Log("Process Exited!");
            _process.CancelOutputRead();
            _process.CancelErrorRead();
        }

    }
}