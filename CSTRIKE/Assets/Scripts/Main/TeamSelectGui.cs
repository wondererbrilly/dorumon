using UnityEngine;
using System.Collections;
using gui = UnityEngine.GUILayout;

public class TeamSelectGui : Bs
{

    public void OnGUI()
    {
        if (Screen.lockCursor) return;
        var c = new Vector3(Screen.width, Screen.height) / 2f;
        var s = new Vector3(200, 200) / 2f;
        var v1 = c - s;
        var v2 = c + s;
        GUI.Window((int)WindowEnum.TeamSelect, Rect.MinMaxRect(v1.x, v1.y, v2.x, v2.y), SelectTeam, "Choose A Class");
    }
    
    bool teamSelected;
    void SelectTeam(int id)
    {
        if (teamSelected)
        {
            if (gui.Button("<<Back"))
                teamSelected = false;
            var p = _Game.PlayerPrefab.GetComponent<Player>();
            var skins = (_Game.team == Team.CounterTerrorists ? p.CTerrorSkins : p.TerrorSkins);
            for (int i = 0; i < skins.Length; i++)
            {
                if (gui.Button(skins[i].name))
                {
                    _Game.PlayerSkin = i;
                    OnTeamSelected();
                }
            }
        }
        else
        {
            if (gui.Button("Terrorists"))
            {
                _Game.team = Team.Terrorists;
                teamSelected = true;
            }
            if (gui.Button("Counter Terrorists"))
            {
                _Game.team = Team.CounterTerrorists;
                teamSelected = true;
            }
            if (gui.Button("Spectator"))
            {
                _Game.team = Team.Spectators;
                OnTeamSelected();
            }
        }
    }

    private void OnTeamSelected()
    {
        teamSelected = false;
        this.enabled = false;
        _Game.OnTeamSelected();
    }
}
