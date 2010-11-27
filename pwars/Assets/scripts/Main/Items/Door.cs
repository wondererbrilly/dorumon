using UnityEngine;
using System.Collections;

public class Door : MapItem
{
    
    public bool opened;
    public override void OnPlayerConnected1(NetworkPlayer np)
    {
        networkView.RPC("RPCOpen", np, opened);
        base.OnPlayerConnected1(np);
    }
    public override void CheckOut()
    {
        this.RPCOpen();
    }
    public override string title()
    {
        return "����� B ����� ������� �����, ����� " + score + " �����";
    }
    [RPC]
    public void RPCOpen()
    {
            CallRPC();
            animation.Play();
            PlaySound("dooropen", 10);
            enabled = false;
    }
}
