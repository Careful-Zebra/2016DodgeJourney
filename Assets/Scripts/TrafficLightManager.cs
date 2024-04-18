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
        state = new int[4];
        state[0] = 0;
        state[1] = 1;
        state[2] = 0; 
        state[3] = 1;
        countdown = m_ChangeTime;
        transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
        transform.GetChild(1).GetComponent<Renderer>().material.color = Color.green;
        transform.GetChild(2).GetComponent<Renderer>().material.color = Color.red;
        transform.GetChild(3).GetComponent<Renderer>().material.color = Color.green;

       
    }

    void Update()
    {
        print(state.ToString());
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            for (int i = 0; i < 4; i++) {

                state[i] = (state[i] + 1) % 3;

                Renderer sr = gameObject.transform.GetChild(i).GetComponent<Renderer>();

                //set colour
                if (state[i] == 0)
                {
                    sr.material.color = Color.red;
                } else if (state[i] == 1)
                {
                    sr.material.color = Color.green;
                    
                } else
                {
                    sr.material.color = Color.yellow;
                }
                countdown = m_ChangeTime;
            }

            
        }
    }

    public int GetState(int dir)
    {
        //again, 0 is red, 1 is green, 2 is amber
        return state[dir];
    }

    public string Type()
    {
        return "Traffic Light";
    }

    #endregion
}
