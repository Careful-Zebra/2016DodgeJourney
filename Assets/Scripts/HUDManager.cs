using System;
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
    [Tooltip("The object that holds all cars in the scene")]
    private CarHolder ch;

    [SerializeField]
    [Tooltip("The text object that displays the average speed of all cars")]
    private TextMeshProUGUI meanCarSpeedText;

    [SerializeField]
    [Tooltip("The text object that displays the current score")]
    private TextMeshProUGUI scoreText;

    //[SerializeField]
    //[Tooltip("The cars on the map")]
    //private CarController[] cars;
    #endregion

    #region Private Variables
    private string meanCarSpeedString;
    private float meanCarSpeed = 0;

    private float score;
    private string scoreString;
    #endregion

    void Awake()
    {
        meanCarSpeedString = meanCarSpeedText.text;
        meanCarSpeedText.text = meanCarSpeedString.Replace("%S", "0");

        score = 0;
        scoreString = scoreText.text;
        scoreText.text = scoreString.Replace("%S", "0");
    }

    void FixedUpdate()
    {
        meanCarSpeed = 0;
        foreach (CarController car in ch.setOfCars)
        {
            Vector2 velocity = car.gameObject.GetComponent<Rigidbody2D>().velocity;
            meanCarSpeed += Math.Abs(velocity.x) + Math.Abs(velocity.y);
        }

        //updating average car velocity
        meanCarSpeed /= ch.setOfCars.Count;
        meanCarSpeedText.text = meanCarSpeedString.Replace("%S", Math.Round(meanCarSpeed, 2).ToString());

        //updating score
        score += Time.deltaTime;
        scoreText.text = scoreString.Replace("%S", Math.Round(score, 2).ToString());
    }
}
