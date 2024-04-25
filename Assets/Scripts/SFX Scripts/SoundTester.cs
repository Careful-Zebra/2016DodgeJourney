using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour {
    private int temp = 0;
    private int hey = 0;
    private bool rapidCrashes = false;


    private void Update() {
        ++temp;

        if (rapidCrashes && temp >= 200) {
            temp = 0;
            SoundManager.instance.CarCrashSFX();
            return;
        }

        if (temp >= 600) {
            temp = 0;
            if (hey % 5 == 0) {
                SoundManager.instance.CarCrashSFX();
            } else if (hey % 5 == 1) {
                SoundManager.instance.CarDestArriveSFX();
            } else if (hey % 5 == 2) {
                SoundManager.instance.PlaceTrafficObjectSFX();
            } else if (hey % 5 == 3) {
                SoundManager.instance.RemoveTrafficObjectSFX();
            } else {
                SoundManager.instance.TrafficLightColorChangeSFX();
            }
            ++hey;
            Debug.Log(hey);
        }
    }
}
