using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTileBehaviour : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The direction of the road")]
    private string m_Direction;

    [SerializeField]
    [Tooltip("Whether this tile has any traffic objects")]
    private Boolean m_HasTrafficObj;

    [SerializeField]
    [Tooltip("The traffic object, if any, that this tile has")]
    private GameObject m_TrafficObj;

    [SerializeField]
    [Tooltip("Whether this tile has a car on it")]
    private Boolean m_Car;

    [SerializeField]
    [Tooltip("The speed limit of this tile")]
    private int m_SpeedLimit;

    [SerializeField]
    [Tooltip("Whether this road is one way")]
    private Boolean m_OneWay;
    #endregion

    #region Private Variables

    //array of cars on this tile
    private ArrayList cars = new ArrayList();

    //all the neighbours of this tile - 0th is one above, 1st is one to the right, 2nd is one below, 3rd is one to the left
    private GameObject[] neighbours = new GameObject[4];

    #endregion

    #region Methods

    /* 
    * mostly getter methods so that the car can have some idea of where it is driving,
    * how it should drive, what it should be looking out for, and so on and so forth.
    */

    public GameObject getTrafficObj()
    {
        if (!m_HasTrafficObj)
        {
            return null;
        }
        else
        {
            return m_TrafficObj;
        }
    }

    public int getSpeed()
    {
        return m_SpeedLimit;
    }

    public void addCar(GameObject car)
    {
        cars.Add(car);
    }

    public void removeCar(GameObject car)
    {
        cars.Remove(car);
    }

    public GameObject getNextTile(int direction)
    {
        //again, 0 is up, 1 is to the right, 2 is down, 3 is to the left
        return neighbours[direction];
    }

    #endregion




}
