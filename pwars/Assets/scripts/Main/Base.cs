﻿using System.Linq;
using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using doru;
using System.Xml.Serialization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Random = UnityEngine.Random;
public class Base : Base2
{    
    public int OwnerID = -1;
    public bool isOwner { get { return OwnerID == Network.player.GetHashCode(); } }
    public void ShowPopup(string s)
    {
        _PopUpWindow.ShowDontHide(_Loader);
        _PopUpWindow.Text = s;
    }
    bool defenabled;
    public virtual void Awake()
    {        
        defenabled = enabled;
        if (networkView != null && enabled)
        {
            name += "+" + Regex.Match(networkView.viewID.ToString(), @"\d+").Value;
            if (Network.peerType == NetworkPeerType.Disconnected)
            {
                enabled = false;
            }
        }
    }
    protected virtual void Start(){}
    protected virtual void OnServerInitialized() { Enable(); }
    protected virtual void OnConnectedToServer() { Enable(); }
    protected virtual void Enable() { if (networkView != null) enabled = defenabled; }

    
    public virtual void OnPlayerConnectedBase(NetworkPlayer np) { }
    public NetworkView myNetworkView
    {
        get
        {
            return GetNetworkView(Network.player);
        }
    }
    public NetworkView GetNetworkView(NetworkPlayer pl)
    {
        foreach (NetworkView b in this.GetComponents<NetworkView>())
            if (b.owner == pl) return b;
        return null;
    }
    public const string hosting = "http://physxwars.rh10.ru/";
    public static bool build { get { return _Loader.build; } }
    public static bool debug { get { return !_Loader.build; } }
    public static bool skip { get { return _Loader.skip; } }
    public static bool isWebPlayer { get { return Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer; } }
    public static Level _Level { get { return _Loader._Level; } set { _Loader._Level = value; } }
    public static bool DebugKey(KeyCode key)
    {
        if (Input.GetKeyDown(key))
            print("Debug Key" + key);
        return Input.GetKeyDown(key);
    }
    public static string joinString(char j, params object[] o)
    {
        string s = "";
        foreach (object a in o)
            s += a.ToString() + j;
        return s.Trim(j);
    }
    public static IEnumerable<Player> TP(Team t)
    {
        foreach (Player p in players)
                if (p != null && p.team == t) yield return p;
    }
    public static bool IsPointed(LayerMask collmask, float len)
    {
        return RayCast(collmask,len).collider != null;
    }
    public static RaycastHit RayCast(LayerMask msk,float len)
    {
        if (!lockCursor) return default(RaycastHit);
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));//new Ray(cam.transform.position, cam.transform.TransformDirection(Vector3.forward));  
        ray.origin = ray.GetPoint(1);
        RaycastHit h;
        Physics.Raycast(ray, out h, len, msk);
        return h;
    }
    public static IEnumerable<Transform> getChild(Transform t)
    {
        if(t!=null)
        for (int i = 0; i < t.childCount; i++)
            yield return t.GetChild(i);
    }
    public static float clamp(float a)
    {
        if (a > 180) return a - 360f;
        return a;
    }
    public static Player[] players { get { return _Game.players; } }
    public void PlaySound(AudioClip au)
    {
        PlaySound(au, 1);
    }
    public void PlaySound(AudioClip au, float volume)
    {
        transform.GetComponentInParrent<AudioSource>().PlayOneShot(au, volume);
    }
    public void PlayRandSound(AudioClip[] au) { PlayRandSound(au, 1); }
    public void PlayRandSound(AudioClip[] au,float volume)
    {        
        if (!transform.root.audio.isPlaying)
            transform.GetComponentInParrent<AudioSource>().audio.PlayOneShot((AudioClip)au[UnityEngine.Random.Range(0, au.Length)], volume);
    }
    public Transform root { get { return this.transform.root; } }
    public void LocalHide() { Show(false); }
    public void LocalShow() { Show(true); }
    public void RPCShow(bool v) { CallRPC("Show",v); }
    [RPC]
    public void Show(bool value)
    {
        Show(this.gameObject, value);
        foreach (Base r in this.GetComponentsInChildren<Base>())
        {
            r.enabled = value;
            r.onShow(value);
        }
    }
    public static void Show(GameObject g, bool value)
    {
        foreach (var rigidbody in g.GetComponentsInChildren<Rigidbody>())
        {
            //rigidbody.isKinematic = !value;
            rigidbody.detectCollisions = value;
            rigidbody.useGravity = value;                        
        }
        foreach (var t in g.GetComponentsInChildren<Transform>())
            t.gameObject.layer = value ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("Ignore Raycast");
    
        foreach (var r in g.GetComponentsInChildren<Renderer>())
            r.enabled = value;
        foreach (var r in g.GetComponentsInChildren<AudioListener>())
            r.enabled = value;
        foreach (var r in g.GetComponentsInChildren<Camera>())
            r.enabled = value;

        //foreach (var r in g.GetComponentsInChildren<AudioSource>())
        //{
        //    r.enabled = value;
        //    if (!value)
        //        r.Stop();
        //    else
        //        if (r.playOnAwake) r.Play();
        //}
        
    }
    
    public virtual void onShow(bool enabled)
    {
    }
    public void WWWSend(string s, Action<string> a)
    {
        WWWSend(s, null, a);
    }
    public void WWWSend(string s, WWWForm form, Action<string> a)
    {
        s = s + "&r=" + Random.value;
        Debug.Log("WWW: " + s);
        var w = form == null ? new WWW(s) : new WWW(s, form); ;
        _TimerA.AddMethod(() => w.isDone, delegate
        {
            if (w.error == "" || w.error == null)
                a(w.text);
            else
                Debug.Log(w.error);
        });
    }
    public static NetworkPlayer? sendto;
    public void CallRPC(string name, params object[] obs)
    {        
        if (new StackTrace().FrameCount > 30) throw new StackOverflowException();
        if (sendto == null)
        {
            networkView.RPC(name, RPCMode.Others, obs);
            this.GetType().GetMethod(name).Invoke(this, obs); // make method public !!!!
        }
        else
            networkView.RPC(name, sendto.Value, obs);
        
    }
}
