using System;
using System.Collections;
using UnityEngine;
using gui = UnityEngine.GUILayout;
using doru;

public class LoaderGui : Bs
{
    
    string ip = "127.0.0.1";
    //string label = "";
    public string version;
    public bool ConnectToMasterServer;
    public void Start()
    {
        Screen.lockCursor = false;
        
            Refresh();
    }
    Timer timer = new Timer();
    public void Update()
    {
        if (timer.TimeElapsed(10000))
            Refresh();
        timer.Update();
    }
    public void OnGUI()
    {
        GUI.skin = _Loader.skin;
        var c = new Vector3(Screen.width, Screen.height) / 2f;
        var s = new Vector3(300, 400) / 2f;
        var v1 = c - s;
        var v2 = c + s;
        GUI.Window((int)WindowEnum.ConnectionGUI, Rect.MinMaxRect(v1.x, v1.y, v2.x, v2.y), ServerList, version);
    }
    

    bool SelectMap;
    public GameType gameType = GameType.TeamDeathMatch;
    void ServerList(int id)
    {
        if (SelectMap)
        {
            if (gui.Button("<<Back"))
                SelectMap = false;

            foreach (var map in _Loader.maps)
            {
                int p = GetLevelLoad(map);
                if (gui.Button(map + (p < 100 ? (" " + p + "%") : "")))
                    if (p == 100)
                        Host(map);
                    else
                        print("Map Not Loaded yet");
            }
        }

        else
        {
            if (Screen.lockCursor) return;
            gui.Label("Name:");
            _Loader.playerName = gui.TextField(_Loader.playerName);            
            gui.Label("Ip Address:");
            ip = gui.TextField(ip);
            gui.BeginHorizontal();
            if (gui.Button("Connect"))
                Network.Connect(ip, port);
            if (gui.Button("Host"))
                SelectMap = true;
            if (gui.Button("Refresh"))
            { 
                //Refresh(); 
            }


            gui.EndHorizontal();
            //gui.BeginHorizontal();
            //gui.Label("GameMode:");
            //gameType = (GameType)gui.SelectionGrid((int)gameType, Enum.GetNames(typeof(GameType)),1);
            //gui.EndHorizontal();
            //gui.Label(label);

            foreach (HostData host in MasterServer.PollHostList())
            {
                int p = GetLevelLoad(host.comment);
                if (gui.Button("Join to " + host.gameName +
                    (host.useNat ? "(NAT)" : "") +
                    (p < 100 ? (" " + p + "%") : "")))
                {
                    if (p == 100)
                    {
                        Print("Trying Connect to " + host.gameName);
                        var er = Network.Connect(host);
                        if (er != NetworkConnectionError.NoError)
                            Print(er + "");
                    }
                    else
                        print("Map Not Loaded yet");

                }
            }
        }
    }

    public override void OnEditorGui()
    {
        version = "Counter Strike V" + DateTime.Now.ToShortDateString();
        base.OnSelectionChanged();
        base.OnEditorGui();
    }
    

    private int GetLevelLoad(string a)
    {        
        return (int)(Application.GetStreamProgressForLevel(a)*100);
    }

    private void Host(string mapName)
    {
        Print("Loading Map");
        SelectMap = false;
        Network.InitializeServer(6, port, !Network.HavePublicAddress());        
        MasterServer.RegisterHost(_LoaderGui.version, _Loader.playerName + "'s game",mapName);
        _Loader.LoadLevel(mapName);
    }

    private void Refresh()
    {
        if (!(isEditor && !ConnectToMasterServer))
        {
            //MasterServer.ClearHostList();
            Debug.LogWarning("Refreshing");
            MasterServer.RequestHostList(_LoaderGui.version);
        }
    }


    public void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Print(msEvent + "");
    }
    public void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        Print(info + "");
    }

    public void Print(string text)
    {
        Debug.LogWarning(text);
        //label = text;
    }
}
