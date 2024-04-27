using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class LevelInfo : MonoBehaviour {

    public Dictionary<string, List<GameObject>> buildings = new Dictionary<string, List<GameObject>>();

    public float initCarSpawnCooldown;

    private float carSpawnCooldown;
    public float CarSpawnCooldown {
        get { return carSpawnCooldown; }
    }

    private float timeToIncreaseSpawnRate = 0;

    private void Awake() {
        // Only a list for house and a list for Office should exist.
        buildings["House"] = new List<GameObject>();
        buildings["Office"] = new List<GameObject>();

        carSpawnCooldown = initCarSpawnCooldown;
    }


    private void Update() {
        timeToIncreaseSpawnRate += Time.deltaTime;
        if (timeToIncreaseSpawnRate >= 1f) {
            timeToIncreaseSpawnRate = 0;
            carSpawnCooldown = carSpawnCooldown * 0.97f;
        }

    }

    /* Called by buildings. Returns true if car should be spawned. Function is in this script bc prabability curve is different for each level. */
    public bool ProbabilisticallySpawnCar() {
        float rand = Random.value;
        return rand > 0.85;
    }
}
