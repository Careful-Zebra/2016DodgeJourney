using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    #region Editor Variables
    [SerializeField]
    [Tooltip("The text object that displays the average speed of all cars")]
    private TextMeshProUGUI meanCarSpeedText;

    [SerializeField]
    [Tooltip("The cars on the map")]
    private CarController[] cars;
    #endregion

    #region Private Variables
    private string defaultCarSpeedString;
    private string meanCarSpeedString;
    private float meanCarSpeed = 0;
    #endregion

    float AbsoluteValue(float num)
    {
        if (num >= 0)
        {
            return num;
        } else
        {
            return -1 * num;
        }
    }

    void Awake()
    {


        defaultCarSpeedString = meanCarSpeedText.text;
        meanCarSpeedText.text = defaultCarSpeedString.Replace("%S", "0");
    }

    void FixedUpdate()
    {
        meanCarSpeed = 0;
        foreach (CarController car in cars)
        {
            Vector2 velocity = car.gameObject.GetComponent<Rigidbody2D>().velocity;
            meanCarSpeed += AbsoluteValue(velocity.x) + AbsoluteValue(velocity.y);
        }
        meanCarSpeed /= cars.Length;
        meanCarSpeedText.text = defaultCarSpeedString.Replace("%S", meanCarSpeed.ToString());
    }
}
