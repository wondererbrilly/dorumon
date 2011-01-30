﻿using System;
using UnityEngine;
public enum SpawnType { zombie, RedSpawn, NoneSpawn, BlueSpawn ,none,ZombieSpawnLocation}
[AddComponentMenu("MapTag")]
public class MapTag : MonoBehaviour
{
    public bool glass;
    public SpawnType SpawnType;
    public void Init()
    {
        tag = "glass";
    }
}