using System.Linq;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using doru;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Net.Sockets;
using System.Text;
using Debug = UnityEngine.Debug;
public enum Level { z1login, z2menu, z4game }
public class Loader : Base,IWindow
{
    public string version;
    public string[] cmd;    
    public int lastLevelPrefix;
    public Dictionary<string, Ping> hdps = new Dictionary<string, Ping>();
    new public bool build;
    new public bool skip;    
    public bool dontcheckwin;
    public bool debugPath;
    public bool disablePathFinding= true;
    public bool logged;
    new public UserView LocalUserV;
    new public Level _Level;
    new public TimerA _TimerA = new TimerA();
    public List<MapSetting> mapsets = new List<MapSetting>();
    public bool dedicated { get { return _Loader.cmd.Contains("-batchmode"); } }
    new public MapSetting mapSettings = new MapSetting();
    [FindAsset("Skin/Skin.guiskin")]
    public GUISkin Skin;
    //new public LayerMask collmask = 1 << 8 | 1 << 9 | 1 << 12 | 1 << 13;
    public static bool loaded;
    public override void Awake()
    {
        if (loaded) return;
        loaded = true;
        Application.targetFrameRate = 40;        
        LocalUserV = gameObject.AddComponent<UserView>();
        base.Awake();        
        DontDestroyOnLoad(this.transform.root);
        networkView.group = 1;
        //if(!isWebPlayer)
        //    cmd = Environment.GetCommandLineArgs();
        mapsets.AddRange(new[]{
            new MapSetting { mapName = "Game", title = "Demo" ,supportedModes = new List<GameMode>() { GameMode.DeathMatch , GameMode.TeamDeathMatch, GameMode.TeamZombieSurvive, GameMode.ZombieSurive } },
            //new MapSetting { mapName = "z5city" , title  = "City" ,supportedModes = new List<GameMode>() { GameMode.DeathMatch } }
        });
    }
    public override void Init()
    {
        version = DateTime.Now.ToString();
        base.Init();
    }
    string curdir { get { return Directory.GetCurrentDirectory(); } }
    protected override void Start()
    {
        print("Version " + version);
        _SettingsWindow.ScreenSize = ToString(Screen.resolutions).ToArray();
        Action("onGraphicQuality");
        if (!isWebPlayer)
        {
            Action("onScreenSize");            
            Directory.CreateDirectory(curdir + "/ScreenShots");
            foreach (var a in Directory.GetFiles(curdir + "/ScreenShots").Reverse().Skip(100))
                File.Delete(a);
        }
    }

    public void Action(string s, params object[] p)
    {
        if (s == "onFullScreen")
            Screen.fullScreen = _SettingsWindow.FullScreen;
        if (s == "onGraphicQuality")
            if (_Cam != null)
                _Cam.onEffect();
        if (s == "onScreenSize")
            if (_SettingsWindow.iScreenSize != -1 && _SettingsWindow.iScreenSize < Screen.resolutions.Length)
            {
                print(pr);
                Resolution r = Screen.resolutions[_SettingsWindow.iScreenSize];
                Screen.SetResolution(r.width, r.height, _SettingsWindow.FullScreen);
            }
        if (s == "onShadows")
            if (_Cam != null)
                _Cam.onEffect();
        if (s == "onReset")
            PlayerPrefs.DeleteAll();
        if (s == "onAtmoSphere")
            if (_Cam != null)
                _Cam.onEffect();

        if (s == "onSao")
            if (_Cam != null)
                _Cam.onEffect();
        if (s == "onBloomAndFlares")
            if (_Cam != null)
                _Cam.onEffect();
        if (s == "onRenderSettings")
            _Cam.onEffect();
        if (s == "onOk")
            _PopUpWindow.Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            _Console.enabled = !_Console.enabled;

        AudioListener.volume = _SettingsWindow.SoundVolume;
        if (Network.sendRate != _SettingsWindow.NetworkSendRate) Network.sendRate = _SettingsWindow.NetworkSendRate;
        if (!isWebPlayer && Input.GetKeyDown(KeyCode.E))
            Application.CaptureScreenshot(curdir + "/ScreenShots/Screenshot" + DateTime.Now.ToFileTime() + ".jpg");

        _TimerA.Update();
        //WWW2.Update();
    }
    

    public void RPCLoadLevel(string level, RPCMode rpcmode)
    {
        print("load Level" + level);
        for (int i = 0; i < 20; i++)
            Network.RemoveRPCsInGroup(i);
        networkView.RPC("LoadLevel", rpcmode, level, lastLevelPrefix + 1);
    }
    [RPC]
    public void LoadLevel(string level, int levelPrefix)
    {
        lastLevelPrefix = levelPrefix;
        Network.SetSendingEnabled(0, false);
        Network.isMessageQueueRunning = false;
        Network.SetLevelPrefix(levelPrefix);
        Application.LoadLevel(level);

    }
    void OnLevelWasLoaded(int level)
    {
        _TimerA.Clear();
        foreach (Ping p in hdps.Values)
            p.DestroyPing();
        hdps.Clear();

        try
        {
            _Level = (Level)Enum.Parse(typeof(Level), Application.loadedLevelName);
        }
        catch { _Level = Level.z4game; }
        Network.isMessageQueueRunning = true;
        Network.SetSendingEnabled(0, true);

    }    
    
    IEnumerable<string> ToString(Resolution[] t)
    {
        foreach (Resolution a in t)
            yield return a.width + "x" + a.height + " " + a.refreshRate;
    }
    public int GetPing(string ip)
    {
        Ping p;
        if (!hdps.ContainsKey(ip))
            hdps.Add(ip, p = new Ping(ip));
        else
            p = hdps[ip];

        return p.time;
    }

}

