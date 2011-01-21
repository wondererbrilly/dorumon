﻿
#pragma warning disable 0169, 0414,649,168
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public partial class Base2:MonoBehaviour
{    
    static TeamSelectWindow __TeamSelectWindow;
    public static TeamSelectWindow _TeamSelectWindow { get { if (__TeamSelectWindow == null) __TeamSelectWindow = (TeamSelectWindow)MonoBehaviour.FindObjectOfType(typeof(TeamSelectWindow)); return __TeamSelectWindow; } }
}

public class TeamSelectWindow : WindowBase {
		
	internal bool vTeamsView = true;
	internal bool focusTeamsView;
	internal bool vTeams = true;
	internal bool focusTeams;
	public string[] Teams = new string[] {"Red Team","Blue Team",};
	internal int iTeams = 0;
	internal bool vImages = true;
	internal bool focusImages;
	internal bool vred = true;
	internal bool focusRed;
	[FindAsset("Image2")]
	public Texture ImageRed;
	internal bool vblue = true;
	internal bool focusBlue;
	[FindAsset("Image1")]
	public Texture ImageBlue;
	internal bool vTeamSelect = true;
	internal bool focusTeamSelect;
	internal bool TeamSelect=false;
	internal bool vfraglimit = true;
	internal bool focusFraglimit;
	internal bool isReadOnlyFraglimit = true;
	internal int Fraglimit = 0;
	internal bool vGameType = true;
	internal bool focusGameType;
	internal bool vZombi = false;
	internal bool focusZombi;
	internal bool vZombiTeam = false;
	internal bool focusZombiTeam;
	internal bool vDeathmatch = false;
	internal bool focusDeathmatch;
	internal bool vTeamDeathMatch = false;
	internal bool focusTeamDeathMatch;
	private int wndid1;
	private Vector2 sTeams;
	private Rect Red;
	private Rect Blue;
	private bool oldMouseOverTeamSelect;
	
    
    
	void Start () {
		wndid1 = UnityEngine.Random.Range(0, 1000);
		Red = new Rect(12f, 2f, 181f, 177f);
		Blue = new Rect(9.573f, 0f, 183.427f, 179f);

	}    
    
    
    bool focusWindow;
    void OnEnable()
    {
        focusWindow = true;
    }
    
    void OnGUI()
    {		
		GUI.skin = _Loader.Skin;
        
		GUI.Window(wndid1,new Rect(-308.5f + Screen.width/2,-197f + Screen.height/2,596f,316f), Wnd1,"");

    }
	void Wnd1(int id){
		if (focusWindow) {GUI.FocusWindow(id);GUI.BringWindowToFront(id);}
		focusWindow = false;
		bool onMouseOver;
		GUI.Label(new Rect(0f, 1f, 99.99f, 14f), @"Game Menu");
		GUI.BeginGroup(new Rect(0f, 19f, 596f, 297f), "");
		GUI.Box(new Rect(0, 0, 596f, 297f), "");
		if(vTeamsView){
		if(focusTeamsView) { focusTeamsView = false; GUI.FocusControl("TeamsView");}
		GUI.SetNextControlName("TeamsView");
		GUI.BeginGroup(new Rect(34f, 19f, 317f, 109f), "");
		GUI.Box(new Rect(0, 0, 317f, 109f), "");
		if(vTeams){
		if(focusTeams) { focusTeams = false; GUI.FocusControl("Teams");}
		GUI.SetNextControlName("Teams");
		GUI.Box(new Rect(11f, 20f, 298f, 87f), "");
		sTeams = GUI.BeginScrollView(new Rect(11f, 20f, 298f, 87f), sTeams, new Rect(0,0, 278f, Teams.Length* 28.9599990844727f));
		int oldTeams = iTeams;
		iTeams = GUI.SelectionGrid(new Rect(0,0, 278f, Teams.Length* 28.9599990844727f), iTeams, Teams,1,GUI.skin.customStyles[0]);
		if (iTeams != oldTeams) Action("Teams",Teams[iTeams]);
		GUI.EndScrollView();
		}
		GUI.EndGroup();
		}
		if(vImages){
		if(focusImages) { focusImages = false; GUI.FocusControl("Images");}
		GUI.SetNextControlName("Images");
		GUI.BeginGroup(new Rect(358f, 20f, 211f, 195f), "");
		GUI.Box(new Rect(0, 0, 211f, 195f), "");
		if(vred){
		if(focusRed) { focusRed = false; GUI.FocusControl("Red");}
		GUI.SetNextControlName("Red");
		if(ImageRed!=null)
			GUI.DrawTexture(Red,ImageRed, ScaleMode.ScaleToFit);
		}
		if(vblue){
		if(focusBlue) { focusBlue = false; GUI.FocusControl("Blue");}
		GUI.SetNextControlName("Blue");
		if(ImageBlue!=null)
			GUI.DrawTexture(Blue,ImageBlue, ScaleMode.ScaleToFit);
		}
		GUI.EndGroup();
		}
		GUI.EndGroup();
		GUI.BeginGroup(new Rect(28f, 263f, 542f, 40f), "");
		GUI.Box(new Rect(0, 0, 542f, 40f), "");
		if(vTeamSelect){
		if(focusTeamSelect) { focusTeamSelect = false; GUI.FocusControl("TeamSelect");}
		GUI.SetNextControlName("TeamSelect");
		bool oldTeamSelect = TeamSelect;
		TeamSelect = GUI.Button(new Rect(449f, 9f, 75f, 21.96f), new GUIContent("Start",""));
		if (TeamSelect != oldTeamSelect && TeamSelect ) {Action("TeamSelect");onButtonClick(); }
		onMouseOver = new Rect(449f, 9f, 75f, 21.96f).Contains(Event.current.mousePosition);
		if (oldMouseOverTeamSelect != onMouseOver && onMouseOver) onOver();
		oldMouseOverTeamSelect = onMouseOver;
		}
		GUI.Label(new Rect(113f, 13.95f, 96f, 21.96f), @"Frag Limit");
		if(vfraglimit){
		if(focusFraglimit) { focusFraglimit = false; GUI.FocusControl("Fraglimit");}
		GUI.SetNextControlName("Fraglimit");
		if(isReadOnlyFraglimit){
		GUI.Label(new Rect(213f, 13.95f, 75.333f, 21.96f), Fraglimit.ToString());
		} else
		Fraglimit = int.Parse(GUI.TextField(new Rect(213f, 13.95f, 75.333f, 21.96f), Fraglimit.ToString()));
		}
		GUI.EndGroup();
		if(vGameType){
		if(focusGameType) { focusGameType = false; GUI.FocusControl("GameType");}
		GUI.SetNextControlName("GameType");
		GUI.BeginGroup(new Rect(40f, 155f, 302f, 95f), "");
		GUI.Box(new Rect(0, 0, 302f, 95f), "");
		if(vZombi){
		if(focusZombi) { focusZombi = false; GUI.FocusControl("Zombi");}
		GUI.SetNextControlName("Zombi");
		GUI.BeginGroup(new Rect(0f, 0f, 292f, 79f), "");
		GUI.Box(new Rect(0, 0, 292f, 79f), "");
		GUI.Label(new Rect(8f, 8f, 276f, 50f), @"Zombie survival. 
 kill the maximum number of zombies.");
		GUI.EndGroup();
		}
		if(vZombiTeam){
		if(focusZombiTeam) { focusZombiTeam = false; GUI.FocusControl("ZombiTeam");}
		GUI.SetNextControlName("ZombiTeam");
		GUI.BeginGroup(new Rect(0f, 0f, 292f, 79f), "");
		GUI.Box(new Rect(0, 0, 292f, 79f), "");
		GUI.Label(new Rect(8f, 8f, 276f, 50f), @"Team battle with zombies. 
 surviving team wins.");
		GUI.EndGroup();
		}
		if(vDeathmatch){
		if(focusDeathmatch) { focusDeathmatch = false; GUI.FocusControl("Deathmatch");}
		GUI.SetNextControlName("Deathmatch");
		GUI.BeginGroup(new Rect(0f, 0f, 292f, 79f), "");
		GUI.Box(new Rect(0, 0, 292f, 79f), "");
		GUI.Label(new Rect(8f, 8f, 276f, 50f), @"DeathMatch. 
 kill the maximum number of players.");
		GUI.EndGroup();
		}
		if(vTeamDeathMatch){
		if(focusTeamDeathMatch) { focusTeamDeathMatch = false; GUI.FocusControl("TeamDeathMatch");}
		GUI.SetNextControlName("TeamDeathMatch");
		GUI.BeginGroup(new Rect(0f, 0f, 292f, 79f), "");
		GUI.Box(new Rect(0, 0, 292f, 79f), "");
		GUI.Label(new Rect(8f, 8f, 276f, 50f), @"Team battle. \r \n kill the maximum number of players.");
		GUI.EndGroup();
		}
		GUI.EndGroup();
		}
		if (GUI.Button(new Rect(596f - 25, 5, 20, 15), "X")) { enabled = false;onButtonClick();Action("Close"); }
	}


	void Update () {
	
	}
}