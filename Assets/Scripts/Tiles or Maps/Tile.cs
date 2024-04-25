using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Editor Variables
    [SerializeField] 
    private Color baseColor;

    [SerializeField] 
    private Color offsetColor;

    [SerializeField] 
    private SpriteRenderer rend;

    [SerializeField] 
    GameObject highlight;

    [SerializeField]
    [Tooltip("Whether this tile is a road tile or not")]
    private Boolean driveable;

    [SerializeField]
    [Tooltip("The traffic light prefab")]
    private GameObject trafficLightPrefab;

    [SerializeField]
    [Tooltip("The stop sign prefab")]
    private GameObject stopSignPrefab;

    //DEBUGGING PURPOSES ONLY
/*    [SerializeField]
    private GameObject trafficObj;
    [SerializeField]
    private Boolean hasTrafficObj;*/

    #endregion

    #region Private Variables
    //the traffic object this tile holds, if any
    private GameObject trafficObj;

    //whether or not this tile has a traffic obj
    private Boolean hasTrafficObj;

    //whether it is valid to place a traffic light on ANY instance of a tile
    private static bool canPlaceTrafficLight = false;

    //whether it is valid to place a stop sign on ANY instance of a tile
    private static bool canPlaceStopSign = false;

    //the string of the traffic obj
    private string trafficObjStr;
    #endregion

    private void Awake()
    {
        // if (driveable)
        // {
        //     GetComponent<Renderer>().material.color = Color.black;
        // }
        highlight.SetActive(false);
    }

    public void Init(bool isOffset)
    {
        // if (isOffset)
        // {
        //     rend.color = offsetColor;
        // } else
        // {
        //     rend.color = baseColor;
        // }
        //rend.color = isOffset ? Color.blue : Color.green;
        rend.color = isOffset ? offsetColor : baseColor;
    }

    #region Mouse Methods

    void OnMouseEnter() // got the idea for the highlighting and general tile generation from a youtube video
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (canPlaceTrafficLight && driveable && !hasTrafficObj)
        {
            hasTrafficObj = true;
            trafficObj = Instantiate(trafficLightPrefab, gameObject.transform, false);
            trafficObjStr = "Traffic Light";

            SoundManager.instance.PlaceTrafficObjectSFX();
        }
        else if (canPlaceStopSign && driveable && !hasTrafficObj) {
            hasTrafficObj = true;
            trafficObj = Instantiate(stopSignPrefab, gameObject.transform, false);
            trafficObjStr = "Stop Sign";

            SoundManager.instance.PlaceTrafficObjectSFX();
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && hasTrafficObj)
        {
            hasTrafficObj = false;
            Destroy(trafficObj);

            SoundManager.instance.RemoveTrafficObjectSFX();
        }
    }

    #endregion

    //method to say if it's driveable or not
    public Boolean Driveable()
    {
        return driveable;
    }

    //getter for the car to check if there is a traffic obj
    public Boolean HasTrafficObj()
    {
        return hasTrafficObj;
    }

    public string TrafficObj()
    {
        return trafficObjStr;
    }

    //Above only gives string type, this gives the obj
    public GameObject TrafficGO()
    {
        return trafficObj;
    }

    // public void spawnCar(GameObject car, int dir, Tile destination, MapHolder holder) {
    //     Quaternion rot;
    //     if (dir == 0) {
    //         rot = Quaternion.Euler(0, 0, 90);
    //     } else if (dir == 1) {
    //         rot = Quaternion.Euler(0, 0, 0);
    //     } else if (dir == 2) {
    //         rot = Quaternion.Euler(0, 0, 270);
    //     } else {
    //         rot = Quaternion.Euler(0, 0, 180);
    //     }
    //     GameObject vroom = Instantiate(car, transform.position, rot);
    //     vroom.GetComponent<ManualDrive>().setDest(destination, holder, dir);
    // }

    #region UI Button Relevant Methods

    public static void PressTrafficLightButton()
    {
        canPlaceTrafficLight = !canPlaceTrafficLight;
        if (canPlaceStopSign) {
            canPlaceStopSign = false;
        }
    }

    public static void PressStopSignButton() {
        canPlaceStopSign = !canPlaceStopSign;
        if (canPlaceTrafficLight) {
            canPlaceTrafficLight = false;
        }
    }
    #endregion

    #region Ella Scene Methods
    public void MakeDriveable()
    {
        driveable = true;
        //rend.color = Color.white;
    }

    public void SetStopSign(GameObject stopSign)
    {
        stopSignPrefab = stopSign;
    }
    #endregion
}
