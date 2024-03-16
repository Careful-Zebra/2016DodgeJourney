using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] 
    private Color baseColor;

    [SerializeField] 
    private Color offsetColor;

    [SerializeField] 
    private SpriteRenderer rend;

    [SerializeField] 
    GameObject highlight;

    private void Awake()
    {
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
}
