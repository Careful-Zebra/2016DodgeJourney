using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StopSignManager : MonoBehaviour
{
    #region Private Variables

    //SortedSet nearbyCars;

    #endregion

    #region Stopping Methods

    void OnCollisionEnter2D(Collision2D coll)
    {
        CarController car = coll.gameObject.GetComponent<CarController>();
        car.Stop();
    }

    #endregion
}
