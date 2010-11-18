﻿using UnityEngine;
using System.Collections;

public class GuiBlood : Base
{
    public float uron = 255f;
    public float repair = .4f;
    void Start()
    {
        
    }
    void Update()
    {

        foreach (GUITexture a in this.GetComponentsInChildren<GUITexture>())
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
