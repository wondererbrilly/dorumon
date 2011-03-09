using UnityEngine;
using System.Collections;
using System.Collections.Generic;
enum btn { Horizontal, Vertical, Rope, Build, EnterCar ,Brake,Rotate,Cut,Destroy,Active}

public class Car : bs {

    public List<GameObject> whells = new List<GameObject>();
    List<WheelCollider> whellColliders = new List<WheelCollider>();
    public bool scnd{get{return pl.scnd;}}
    public Player pl;
    
    public override void Awake()
    {
        Game.cars.Add(this);
        foreach (GameObject t in whells)
        {
            GameObject wg = new GameObject(t.name + "WhellColl");
            wg.transform.parent = transform;
            wg.transform.position = t.transform.position;
            wg.transform.rotation = t.transform.rotation;

            var w = wg.AddComponent<WheelCollider>();
            w.radius = t.transform.localScale.y / 2;
            w.suspensionSpring = new JointSpring { damper = 50, spring = 55000, targetPosition = 0 };
            w.forwardFriction = new WheelFrictionCurve { asymptoteSlip = 2, asymptoteValue = 10000, extremumSlip = 1, extremumValue = 20000, stiffness = 0.092f };
            w.sidewaysFriction = new WheelFrictionCurve { asymptoteSlip = 2, asymptoteValue = 10000, extremumSlip = 1, extremumValue = 20000, stiffness = 0.022f };
            w.suspensionDistance = 0.2f;
            whellColliders.Add(w);
        }  
    }
	void Start () {               
	}
    
    internal float torq;
    internal bool playerIn;
    public void EnterCar(bool enter,Player p)
    {
        Debug.Log("Enter CAr"+ enter);
        if (!enter)
        {
            Game.iplayer = pl;
            pl.pos2 = this.pos2;
            pl.SetActive(true);
            playerIn = false;
            pl = null;
        }
        else
        {
            Game.iplayer = this;
            p.SetActive(false);
            playerIn = true;
            pl = p;
        }
    }
	void Update () {


        if (!playerIn)
        {
            foreach(var wc in whellColliders)
                wc.brakeTorque = 500;
            return;
        }
        
        if (Input.GetButtonDown(btn.EnterCar+""))
        {
            EnterCar(false, null);
        }
        for (int i = 0; i < whells.Count; i++)
        {
            
            var wc = whellColliders[i];
            if (Input.GetButton(btn.Brake+""))
                wc.brakeTorque = 60;
            else
                wc.brakeTorque = 0;
            RaycastHit hit;
            Vector3 ccp = wc.transform.position;
            if (Physics.Raycast(ccp, -wc.transform.up, out  hit, wc.suspensionDistance + wc.radius))
                whells[i].transform.position = hit.point + (wc.transform.up * wc.radius);
            else
                whells[i].transform.position = ccp - (wc.transform.up * wc.suspensionDistance);
        }
        if (Input.GetAxis(btn.Vertical+"") < 0)
            torq = 30;
        else
            torq = 0;

        var trq = 10000;

        rigidbody.AddTorque(0, 0, Input.GetAxis(btn.Horizontal + "")*trq);
        whellColliders[0].motorTorque = torq;
        if(Input.GetButtonDown(btn.Rotate+""))
        {            
            transform.Rotate(Vector3.up, 180);            
        }

	}

}
