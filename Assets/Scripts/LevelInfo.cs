using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LevelInfo : MonoBehaviour {

    public Dictionary<string, List<GameObject>> buildings = new Dictionary<string, List<GameObject>>();

    private void Awake() {
        // Only a list for house and a list for Office should exist.
        buildings["House"] = new List<GameObject>();
        buildings["Office"] = new List<GameObject>();
    }

    /* Called by buildings. Returns true if car should be spawned. Function is in this scirpt bc prabability curve is different for each level. */
    public bool ProbabilisticallySpawnCar() {
        return true;
    }
}
