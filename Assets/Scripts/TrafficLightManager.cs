using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{

    #region Editor Variables

    [SerializeField]
    [Tooltip("The time per light change")]
    private float m_ChangeTime;

    #endregion

    #region Private Variables

    //list of current states of light (0 is red, 1 is green, 2 is amber) [index 0 is N, index 1 is E, index 2 is S, index 3 is W]
    private int[] state;

    //the timer used to change light
    private float countdown;

    #endregion

    #region Methods

    void Start()
    {
        state[0] = 0;
        countdown = m_ChangeTime;
        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    void Update()
    {
        countdown = countdown - Time.deltaTime;
        if (countdown < 0)
        {
            state[0] = (state[0] + 1) % 3;

            Renderer sr = gameObject.GetComponent<Renderer>();

            //set colour
            if (state[0] == 0)
            {
                sr.material.color = Color.red;
                countdown = m_ChangeTime;
            } else if (state[0] == 1)
            {
                sr.material.color = Color.green;
                countdown = m_ChangeTime;
            } else
            {
                sr.material.color = Color.yellow;
                countdown = m_ChangeTime / 2;
            }

            
        }
    }

    public int GetState()
    {
        //again, 0 is red, 1 is green, 2 is amber
        return state[0];
    }

    public string Type()
    {
        return "Traffic Light";
    }

    #endregion
}
