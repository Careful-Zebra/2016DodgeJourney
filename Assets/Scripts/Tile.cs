using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    #region Editor Variables
    [SerializeField] 
    private Color baseColor;

    [SerializeField] 
    private Color offsetColor;

    [SerializeField] 
    private SpriteRenderer rend;

    [SerializeField] 
    GameObject highlight;

    [SerializeField]
    [Tooltip("Whether this tile is a road tile or not")]
    private Boolean driveable;

    #endregion

    private void Awake()
    {
        if (driveable)
        {
            GetComponent<Renderer>().material.color = new Color(31f, 171f, 190f);
        }
        highlight.SetActive(false);
    }

    public void Init(bool isOffset)
    {
        // if (isOffset)
        // {
        //     rend.color = offsetColor;
        // } else
        // {
        //     rend.color = baseColor;
        // }
        //rend.color = isOffset ? Color.blue : Color.green;
        rend.color = isOffset ? offsetColor : baseColor;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    //method to say if it's driveable or not
    public Boolean Driveable()
    {
        return driveable;
    }
}
