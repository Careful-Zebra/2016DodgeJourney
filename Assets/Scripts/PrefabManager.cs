using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The stop sign prefab because the tiles in this scene don't have it by default")]
    GameObject stopSign;

    [SerializeField]
    [Tooltip("The explosion sprite that spawns and despawns after a collision")]
    GameObject pow;


    #region Getter Methods

    public GameObject GiveMeAStopSignPrefab()
    {
        return stopSign;
    }

    public GameObject GiveMeAnExplosion()
    {
        return pow;
    }

    #endregion
}

internal class SerilaizeFieldAttribute : Attribute
{
}