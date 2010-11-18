
#pragma warning disable 649
#pragma warning disable 168
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public partial class Base2:MonoBehaviour
{    
    static LoginWindow __LoginWindow;
    public static LoginWindow _LoginWindow { get { if (__LoginWindow == null) __LoginWindow = (LoginWindow)MonoBehaviour.FindObjectOfType(typeof(LoginWindow)); return __LoginWindow; } }
}

public class LoginWindow : WindowBase {
		
	public string Nick{ get { return PlayerPrefs.GetString("Nick", @""); } set { PlayerPrefs.SetString("Nick", value); } }
	internal int tabTabControl4;
	internal bool focusEnterAsGuest;
	internal bool EnterAsGuest=false;
	internal bool focusNick;
	internal bool isReadOnlyNick = false;
	internal bool focusAddress;
	internal bool isReadOnlyAddress = false;
	internal string Address = @"";
	internal bool Button9=false;
	internal bool Button10=false;
	private int wndid1;
	private bool oldMouseOverEnterAsGuest;
	private bool oldMouseOverButton9;
	private bool oldMouseOverButton10;
	
    
    
	void Start () {
		wndid1 = UnityEngine.Random.Range(0, 1000);

	}    
    
    
    bool focusWindow;
    void OnEnable()
    {
        focusWindow = true;
    }
    
    void OnGUI()
    {		
		GUI.skin = (GUISkin)Resources.Load("Skin/Skin");
        
		GUI.Window(wndid1,new Rect(-302.5f + Screen.width/2,-192f + Screen.height/2,612f,346f), Wnd1,"");

    }
	void Wnd1(int id){
		if (focusWindow) {GUI.FocusWindow(id);GUI.BringWindowToFront(id);}
		focusWindow = false;
		bool onMouseOver;
		GUI.BeginGroup(new Rect(19f, 21f, 574f, 88f), "");
		GUI.Box(new Rect(0, 0, 574f, 88f), "");
		GUI.Label(new Rect(25f, 22f, 524f, 43f), @"������ ��� ������� � �������� ���� ���� � ������ ������, ���������� ��������� ���� (����������� � �������).");
		GUI.EndGroup();
		GUI.BeginGroup(new Rect(19f, 130f, 562f, 199f), "");
		GUI.Box(new Rect(0, 0, 562f, 199f), "");
		GUILayout.BeginArea(new Rect(0f, 0, 562, 18));
		tabTabControl4 = GUILayout.Toolbar(tabTabControl4, new string[] { "����� ��� �����","���� ���������", }, GUI.skin.customStyles[1], GUILayout.ExpandWidth(false));
		GUILayout.EndArea();
		GUI.BeginGroup(new Rect(0, 18, 562, 181), "");
		GUI.Box(new Rect(0, 0, 562, 181), "");
		if(tabTabControl4==0){
		GUI.Label(new Rect(21f, 21.04f, 513f, 76f), @"��������: ���� � ������ ""�����"" ������ ��� ������ ������������ ����,����� ��� ������� � �������� ������� � ��������� ������.

��� ����� ������� ���� ���(���) � ��������� ������ ""���"" � ������� ������ ""����"".");
		GUI.Label(new Rect(60f, 114.04f, 33f, 19f), @"���");
		if(focusEnterAsGuest) { focusEnterAsGuest = false; GUI.FocusControl("EnterAsGuest");}
		GUI.SetNextControlName("EnterAsGuest");
		bool oldEnterAsGuest = EnterAsGuest;
		EnterAsGuest = GUI.Button(new Rect(376f, 114.04f, 107f, 19f), new GUIContent("����",""));
		if (EnterAsGuest != oldEnterAsGuest && EnterAsGuest ) {Action("onEnterAsGuest");onButtonClick(); }
		onMouseOver = new Rect(376f, 114.04f, 107f, 19f).Contains(Event.current.mousePosition);
		if (oldMouseOverEnterAsGuest != onMouseOver && onMouseOver) onOver();
		oldMouseOverEnterAsGuest = onMouseOver;
		if(focusNick) { focusNick = false; GUI.FocusControl("Nick");}
		GUI.SetNextControlName("Nick");
		if(isReadOnlyNick){
		GUI.Label(new Rect(97f, 114.04f, 260f, 19f), Nick.ToString());
		} else
		Nick = GUI.TextField(new Rect(97f, 114.04f, 260f, 19f), Nick,10);
		}
		if(tabTabControl4==1){
		GUI.Label(new Rect(58f, 113f, 59f, 19f), @"�����:");
		if(focusAddress) { focusAddress = false; GUI.FocusControl("Address");}
		GUI.SetNextControlName("Address");
		if(isReadOnlyAddress){
		GUI.Label(new Rect(112f, 109.04f, 344f, 17.96f), Address.ToString());
		} else
		Address = GUI.TextField(new Rect(112f, 109.04f, 344f, 17.96f), Address);
		GUI.Label(new Rect(28f, 19f, 517f, 85f), @"1. ������� �� ������: 

2. ���������� ��� �� �������� ������ ������������ ���� ""Login sucess""

3. �������� ������������� ����� ���� � ������ ""�����"" � ������� ������ ""����"".");
		bool oldButton9 = Button9;
		Button9 = GUI.Button(new Rect(189f, 17f, 101f, 18f), new GUIContent("�����������",""));
		if (Button9 != oldButton9 && Button9 ) {Action("onButton9");onButtonClick(); }
		onMouseOver = new Rect(189f, 17f, 101f, 18f).Contains(Event.current.mousePosition);
		if (oldMouseOverButton9 != onMouseOver && onMouseOver) onOver();
		oldMouseOverButton9 = onMouseOver;
		bool oldButton10 = Button10;
		Button10 = GUI.Button(new Rect(464f, 110f, 44f, 18f), new GUIContent("����",""));
		if (Button10 != oldButton10 && Button10 ) {Action("onButton10");onButtonClick(); }
		onMouseOver = new Rect(464f, 110f, 44f, 18f).Contains(Event.current.mousePosition);
		if (oldMouseOverButton10 != onMouseOver && onMouseOver) onOver();
		oldMouseOverButton10 = onMouseOver;
		}
		GUI.EndGroup();
		GUI.EndGroup();
	}


	void Update () {
	
	}
}