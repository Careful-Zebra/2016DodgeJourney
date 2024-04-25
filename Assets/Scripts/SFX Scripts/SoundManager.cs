using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager instance { get; private set; }

    #region Serialized vars
    [SerializeField] private AudioSource[] carCrashSounds;
    [SerializeField] private AudioSource carArriveSound;
    [SerializeField] private AudioSource placeTraffObjSound;
    [SerializeField] private AudioSource removeTraffObjSound;
    [SerializeField] private AudioSource TraffLightChangeSound;

    [SerializeField] private float LowPitchRange;
    [SerializeField] private float HighPitchRange;
    #endregion


    private void Awake() {
        /* Initialize singleton and ensure only 1 instance of this class exists */
        if (instance != null && instance != this) {
            Destroy(this);
        } else {
            instance = this;
        }
    }

    public void CarCrashSFX() {
        int randIndex = Random.Range(0, carCrashSounds.Length);
        float randPitch = Random.Range(LowPitchRange, HighPitchRange);

        carCrashSounds[randIndex].pitch = randPitch;
        carCrashSounds[randIndex].Play();
    }

    public void CarDestArriveSFX() {
        carArriveSound.Play();
    }

    public void PlaceTrafficObjectSFX() {
        placeTraffObjSound.Play();
    }

    public void RemoveTrafficObjectSFX() {
        removeTraffObjSound.Play();
    }

    public void TrafficLightColorChangeSFX() {
        TraffLightChangeSound.Play();
    }

}
