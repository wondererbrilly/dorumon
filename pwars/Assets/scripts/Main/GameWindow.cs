using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class GameWindow : Base2 {

    public GUIText fpstext;
    public GUIText chatInput;
    public GUIText chatOutput;
    public GUIText CenterText;
    public GUITexture gunTexture;
    public GUIText gunPatrons;
    public GUITexture BlueBar;
    public GUIText Score;
    public GUITexture RedBar;
    public GUIText blueTeam;
    public GUIText redTeam;
    public GUIText time;
    public GUIText systemMessages;
    public GUIText level;
    public GUIText zombiesLeft;
    public GUIText frags;    
    public float energy
    {
        get { return BlueBar.pixelInset.width / 2; }
        set
        {
            Rect r = BlueBar.pixelInset;
            r.width = Mathf.Min(value * 2, 200); ;
            BlueBar.pixelInset = r;
        }
    }
    public float life
    {
        get { return RedBar.pixelInset.width / 2; }
        set
        {
            Rect r = RedBar.pixelInset;
            r.width = Mathf.Min(value * 2, 200);
            RedBar.pixelInset = r;
        }
    }
    public List<GUITexture> blood;
    public float uron = 255f;
    public float repair = .4f;        
    
    List<string> systemMessage = new List<string>();
    public void AppendSystemMessage(string s)
    {
        systemMessage.Insert(0, s);
        systemMessages.text = string.Join("\r\n", systemMessage.Take(5).Reverse().ToArray());
    }
    void AppendChatMessage(string s)
    {

    }

    void Start()
    {
        foreach(GUIText text in GetComponentsInChildren<GUIText>())
            if (!text.text.StartsWith(" ")) text.text = "";
    }

    public int fps;
    void Update()
    {
        
        if (_TimerA.TimeElapsed(500))
            fps = (int)_TimerA.GetFps();
        fpstext.text = "Fps: " + fps + " Errors:" + _Console.errorcount;

        var ts = System.TimeSpan.FromMinutes(_Game.timeleft);
        _GameWindow.time.text = ts.Minutes + ":" + ts.Seconds;
        if (mapSettings.Team)
        {
            _GameWindow.blueTeam.text = _Game.BlueFrags.ToString();
            _GameWindow.redTeam.text = _Game.RedFrags.ToString();
        }

        if (_localiplayer != null)
        {
            _GameWindow.life = _localiplayer.Life;
            _GameWindow.gunPatrons.text = _localPlayer.gun.name + ":" + _localPlayer.gun.patronsLeft;

            _GameWindow.energy = (int)_localiplayer.nitro;
            if (mapSettings.zombi)
            {
                _GameWindow.zombiesLeft.text = "�����" + _Game.AliveZombies.Count().ToString();
                _GameWindow.level.text = "�������" + _Game.stage.ToString();
            }
            _GameWindow.frags.text = "����� " + _localPlayer.frags.ToString();

            _GameWindow.Score.text = "���� " + _localPlayer.score.ToString();
        }

        foreach (GUITexture a in blood)
            if (a.guiTexture.color.a > 0)
                a.guiTexture.color -= new Color(0, 0, 0, Time.deltaTime * repair);
    }
    public void Hit(int hit)
    {
        GUITexture[] gs = this.GetComponentsInChildren<GUITexture>();
        GUITexture g = gs[Random.Range(0, gs.Length - 1)];
        g.color += new Color(0, 0, 0, Mathf.Min(.7f, hit / uron));
    }

}

