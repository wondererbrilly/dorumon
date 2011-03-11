﻿using System;
using UnityEngine;
[AddComponentMenu("Base")]
public class bs : Base
{
    
    public Vector2 pos2 { get { return new Vector2(transform.position.x, transform.position.y); } set { transform.position = new Vector3(value.x, value.y, transform.position.z); } }
    public bool attachRope;    
    static Game _Game;
    public static Game Game { get { if (_Game == null) _Game = (Game)MonoBehaviour.FindObjectOfType(typeof(Game)); return _Game; } }
    static Cam _Cam;
    public static Cam Cam { get { if (_Cam == null) _Cam = (Cam)MonoBehaviour.FindObjectOfType(typeof(Cam)); return _Cam; } }
    static GameGui _GameGui;
    public static GameGui GameGui { get { if (_GameGui == null) _GameGui = (GameGui)MonoBehaviour.FindObjectOfType(typeof(GameGui)); return _GameGui; } }
    static Player _Player;
    public static Player Player { get { if (_Player == null) _Player = (Player)MonoBehaviour.FindObjectOfType(typeof(Player)); return _Player; } }
    public virtual void AlwaysUpdate() { }
}