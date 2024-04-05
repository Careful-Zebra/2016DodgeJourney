using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour {
    
    /* Called by buildings. Returns true if car should be spawned. */
    public bool ProbabilisticallySpawnCar() {
        return true;
    }
}
