using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class StopSignManager : MonoBehaviour
{
    #region Private Variables

    Queue cars;

    #endregion

    void Start() {
        cars = new Queue();
    }

    void Update() {
        if (cars.Count != 0) {
            GameObject frontCar = (GameObject) cars.Peek();
            print("frontCar.GetComponent<ManualDrive>().curTile()");
            print(frontCar.GetComponent<ManualDrive>().curTile());
            print("transform.parent");
            print(transform.parent);
            if (frontCar.GetComponent<ManualDrive>().curTile().transform != transform.parent) {
                cars.Dequeue();
            }
            print("removing");
        }
    }

    #region Stopping Methods

    public Boolean CanIGo(GameObject asker) {
        // if (cars.Count > 0) { Debug.Log(cars.Peek()); } else { Debug.Log("Empty!"); }
        if (!cars.Contains(asker)) {
            print("adding");
            cars.Enqueue(asker);
            return false;
        } else {
            if ((GameObject) cars.Peek() == asker) {
                return true;
            }
        }
        return false;
    }

    #endregion
}
