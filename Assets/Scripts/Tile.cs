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

    //whether or not it is valid to place a traffic light on ANY instance of a tile
    private static bool canPlaceTrafficLight = false;
    #endregion

    private void Awake()
    {
        /*if (driveable)
        {
            GetComponent<Renderer>().material.color = new Color(31f, 171f, 190f);
        }*/
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

    void OnMouseEnter()
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
        }

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hasTrafficObj = false;
            Destroy(trafficObj);
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
        //will need to be updated to accommodate different traffic objects
        return trafficObj.GetComponent<TrafficLightManager>().Type();
    }

    //Above only gives string type, this gives the obj
    public GameObject TrafficGO()
    {
        return trafficObj;
    }

    #region UI Button Relevant Methods

    public static void PressTrafficLightButton()
    {
        canPlaceTrafficLight = !canPlaceTrafficLight;
    }
    #endregion

    #region Ella Scene Methods
    public void MakeDriveable()
    {
        driveable = true;
        rend.color = Color.white;
    }
    #endregion
}
