using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHolder : MonoBehaviour
{
    #region Public Variables
    public HashSet<CarController> setOfCars = new HashSet<CarController>();
    #endregion

    #region Methods

    void AddChildrenToSet()
    {
        CarController[] childCars = GetComponentsInChildren<CarController>();
        foreach (CarController childCar in childCars)
        {
            setOfCars.Add(childCar);
        }
    }


    public void AddCarToSet(CarController carc)
    {
        setOfCars.Add(carc);
    }

    public void RemoveCarFromSet(CarController carc)
    {
        setOfCars.Remove(carc);
    }

    #endregion

    #region Initialization (with a z)

    void Awake()
    {
        AddChildrenToSet();
    }

    #endregion

    #region Updates
    #endregion
}
