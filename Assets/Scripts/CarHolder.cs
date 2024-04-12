using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHolder : MonoBehaviour
{
    #region Public Variables
    public HashSet<CarController> setOfCars = new HashSet<CarController>();
    #endregion

    #region Methods

    void AddChildrenToList()
    {
        CarController[] childCars = GetComponentsInChildren<CarController>();
        foreach (CarController childCar in childCars)
        {
            setOfCars.Add(childCar);
        }
    }
    #endregion

    #region Initialization (with a z)

    void Awake()
    {
        AddChildrenToList();
    }

    #endregion

    #region Updates
    #endregion
}
