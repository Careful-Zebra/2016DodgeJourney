using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{

    #region Editor Variables

    [SerializeField]
    [Tooltip("The time per light change")]
    private int m_ChangeTime;

    #endregion

    #region Private Variables

    //current state of light (0 is red, 1 is green, 2 is amber)
    private int state;

    //the timer used to change light
    private float countdown;

    #endregion

    #region Methods

    void Start()
    {
        state = 0;
        countdown = m_ChangeTime;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        countdown = countdown - Time.deltaTime;

        if (countdown < 0)
        {
            state = (state + 1) % 3;

            Renderer sr = gameObject.GetComponent<Renderer>();

            //set colour
            if (state == 0)
            {
                sr.material.color = Color.red;
            } else if (state == 1)
            {
                sr.material.color = Color.green;
            } else
            {
                sr.material.color = Color.yellow;
            }

            countdown = m_ChangeTime;
        }
    }

    public int getState()
    {
        //again, 0 is red, 1 is green, 2 is amber
        return state;
    }

    #endregion
}
