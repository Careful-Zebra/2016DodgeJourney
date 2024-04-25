using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The stop sign prefab because the tiles in this scene don't have it by default")]
    GameObject stopSign;


    #region Getter Methods

    public GameObject GiveMeAStopSignPrefab()
    {
        return stopSign;
    }

    #endregion
}
