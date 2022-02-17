using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBrowser : MonoBehaviour
{
    #region General

    [Header("General settings")]
    public int Width = 1024;
    public int Height = 768;
    public string MemoryFile = "MainSharedMem";
    public bool SwapVericalAxis = false;
    public bool SwapHorisontalAxis = false;
    public bool RandomMemoryFile = true;
    public string InitialURL = "http://www.google.com";
    public string StreamingResourceName = string.Empty;

    public bool EnableWebRTC;

    [Header("Testing")] public bool EnableGPU;

    [Multiline] public string JSInitializationCode = "";

    #endregion
}
