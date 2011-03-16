﻿
#pragma warning disable 0169, 0414,649,168
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public partial class Base:MonoBehaviour
{    
    static MenuWindow __MenuWindow;
    public static MenuWindow _MenuWindow { get { if (__MenuWindow == null) __MenuWindow = (MenuWindow)MonoBehaviour.FindObjectOfType(typeof(MenuWindow)); return __MenuWindow; } }
}
public enum MenuWindowEnum { SelectLevel,QualitySettings,DisableSounds,DisableMusic,NewGame,Disable_Tips,Close, }
public class MenuWindow : WindowBase {
		
	
	internal bool vSelectLevel = true;
	
	internal bool focusSelectLevel;
	public string[] lSelectLevel;
	internal int iSelectLevel = -1;
	public string SelectLevel { get { if(lSelectLevel.Length==0 || iSelectLevel == -1) return ""; return lSelectLevel[iSelectLevel]; } set { iSelectLevel = lSelectLevel.SelectIndex(value); }}
	
	internal bool vQualitySettings = true;
	
	internal bool focusQualitySettings;
	public string[] lQualitySettings;
	internal int iQualitySettings = -1;
	public string QualitySettings { get { if(lQualitySettings.Length==0 || iQualitySettings == -1) return ""; return lQualitySettings[iQualitySettings]; } set { iQualitySettings = lQualitySettings.SelectIndex(value); }}
	
	internal bool vDisableSounds = true;
	
	internal bool focusDisableSounds;
	internal bool DisableSounds=false;
	
	internal bool vDisableMusic = true;
	
	internal bool focusDisableMusic;
	internal bool DisableMusic=false;
	
	internal bool vMouseSensivity = true;
	
	internal bool focusMouseSensivity;
	internal float MouseSensivity = 1f;
	
	internal bool vNewGame = true;
	
	internal bool focusNewGame;
	internal bool NewGame=false;
	
	internal bool vDisable_Tips = true;
	
	internal bool focusDisable_Tips;
	internal bool Disable_Tips { get { return PlayerPrefs.GetInt("Disable_Tips", 0) == 1; } set { PlayerPrefs.SetInt("Disable_Tips", value?1:0); } }
	private int wndid1;
	private Vector2 sSelectLevel;
	private Vector2 sQualitySettings;
	private bool oldMouseOverDisableSounds;
	private bool oldMouseOverDisableMusic;
	private bool oldMouseOverNewGame;
	private bool oldMouseOverDisable_Tips;
	
    
    
	void Start () {
		AlwaysOnTop = false;
		wndid1 = 0;

	}    
    
    
    bool focusWindow;
    void OnEnable()
    {
        focusWindow = true;
    }
    public override void ResetValues()
    {
		vSelectLevel = true;
		iSelectLevel = -1;
		vQualitySettings = true;
		iQualitySettings = -1;
		vDisableSounds = true;
		vDisableMusic = true;
		vMouseSensivity = true;
		vNewGame = true;
		vDisable_Tips = true;

        base.ResetValues();
    }
    public override void OnGUI()
    {		
		base.OnGUI();
        
		GUI.Window(wndid1,new Rect(-240.5f + Screen.width/2,-263f + Screen.height/2,486f,467f), Wnd1,"");

    }
	void Wnd1(int id){
		if (focusWindow) {GUI.FocusWindow(id);GUI.BringWindowToFront(id);}
		focusWindow = false;
		bool onMouseOver;
		if(vSelectLevel){
		if(focusSelectLevel) { focusSelectLevel = false; GUI.FocusControl("SelectLevel");}
		GUI.SetNextControlName("SelectLevel");
		GUI.Box(new Rect(61f, 106f, 203f, 308f), "");
		sSelectLevel = GUI.BeginScrollView(new Rect(61f, 106f, 203f, 308f), sSelectLevel, new Rect(0,0, 193f, lSelectLevel.Length* 15f));
		int oldSelectLevel = iSelectLevel;
		iSelectLevel = GUI.SelectionGrid(new Rect(0,0, 193f, lSelectLevel.Length* 15f), iSelectLevel, lSelectLevel,1,GUI.skin.customStyles[0]);
		if (iSelectLevel != oldSelectLevel) Action(MenuWindowEnum.SelectLevel);
		GUI.EndScrollView();
		}
		if(vQualitySettings){
		if(focusQualitySettings) { focusQualitySettings = false; GUI.FocusControl("QualitySettings");}
		GUI.SetNextControlName("QualitySettings");
		GUI.Box(new Rect(328f, 107.96f, 121f, 151f), "");
		sQualitySettings = GUI.BeginScrollView(new Rect(328f, 107.96f, 121f, 151f), sQualitySettings, new Rect(0,0, 111f, lQualitySettings.Length* 15f));
		int oldQualitySettings = iQualitySettings;
		iQualitySettings = GUI.SelectionGrid(new Rect(0,0, 111f, lQualitySettings.Length* 15f), iQualitySettings, lQualitySettings,1,GUI.skin.customStyles[0]);
		if (iQualitySettings != oldQualitySettings) Action(MenuWindowEnum.QualitySettings);
		GUI.EndScrollView();
		}
		if(vDisableSounds){
		if(focusDisableSounds) { focusDisableSounds = false; GUI.FocusControl("DisableSounds");}
		GUI.SetNextControlName("DisableSounds");
		bool oldDisableSounds = DisableSounds;
		DisableSounds = GUI.Toggle(new Rect(333f, 293f, 98.18667f, 15.96f),DisableSounds, new GUIContent(@"Disable Sounds",""));
		if (DisableSounds != oldDisableSounds ) {Action(MenuWindowEnum.DisableSounds);onButtonClick(); }
		onMouseOver = new Rect(333f, 293f, 98.18667f, 15.96f).Contains(Event.current.mousePosition);
		if (oldMouseOverDisableSounds != onMouseOver && onMouseOver) onOver();
		oldMouseOverDisableSounds = onMouseOver;
		}
		if(vDisableMusic){
		if(focusDisableMusic) { focusDisableMusic = false; GUI.FocusControl("DisableMusic");}
		GUI.SetNextControlName("DisableMusic");
		bool oldDisableMusic = DisableMusic;
		DisableMusic = GUI.Toggle(new Rect(333f, 322f, 90.15334f, 15.96f),DisableMusic, new GUIContent(@"Disable Music",""));
		if (DisableMusic != oldDisableMusic ) {Action(MenuWindowEnum.DisableMusic);onButtonClick(); }
		onMouseOver = new Rect(333f, 322f, 90.15334f, 15.96f).Contains(Event.current.mousePosition);
		if (oldMouseOverDisableMusic != onMouseOver && onMouseOver) onOver();
		oldMouseOverDisableMusic = onMouseOver;
		}
		if(vMouseSensivity){
		if(focusMouseSensivity) { focusMouseSensivity = false; GUI.FocusControl("MouseSensivity");}
		GUI.SetNextControlName("MouseSensivity");
		MouseSensivity = GUI.HorizontalSlider(new Rect(305f, 377f, 159f, 27f), MouseSensivity, 0f, 2f);
		GUI.Label(new Rect(464f,377f,40,15),System.Math.Round(MouseSensivity,1).ToString());
		}
		if(vNewGame){
		if(focusNewGame) { focusNewGame = false; GUI.FocusControl("NewGame");}
		GUI.SetNextControlName("NewGame");
		bool oldNewGame = NewGame;
		NewGame = GUI.Button(new Rect(68f, 57f, 185f, 27f), new GUIContent(@"Start New Game",""));
		if (NewGame != oldNewGame && NewGame ) {Action(MenuWindowEnum.NewGame);onButtonClick(); }
		onMouseOver = new Rect(68f, 57f, 185f, 27f).Contains(Event.current.mousePosition);
		if (oldMouseOverNewGame != onMouseOver && onMouseOver) onOver();
		oldMouseOverNewGame = onMouseOver;
		}
		GUI.Label(new Rect(129f, 90f, 43f, 21.96f), @"Levels");
		GUI.Label(new Rect(366f, 91.96f, 49f, 21.96f), @"Quality");
		GUI.Label(new Rect(314f, 361f, 135f, 21.96f), @"Mouse Sensivity");
		if(vDisable_Tips){
		if(focusDisable_Tips) { focusDisable_Tips = false; GUI.FocusControl("Disable_Tips");}
		GUI.SetNextControlName("Disable_Tips");
		bool oldDisable_Tips = Disable_Tips;
		Disable_Tips = GUI.Toggle(new Rect(61f, 429.96f, 80.38333f, 15.96f),Disable_Tips, new GUIContent(@"Disable Tips",""));
		if (Disable_Tips != oldDisable_Tips ) {Action(MenuWindowEnum.Disable_Tips);onButtonClick(); }
		onMouseOver = new Rect(61f, 429.96f, 80.38333f, 15.96f).Contains(Event.current.mousePosition);
		if (oldMouseOverDisable_Tips != onMouseOver && onMouseOver) onOver();
		oldMouseOverDisable_Tips = onMouseOver;
		}
		if (GUI.Button(new Rect(486f - 25, 5, 20, 15), "X")) { enabled = false;onButtonClick();Action(MenuWindowEnum.Close); }
	}


	void Update () {
	
	}
}