using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MapHolder : MonoBehaviour
{
    private Dictionary<Vector2, Tile> tiles;
    
    private Tile[] childTiles;

    private void Awake()
    {
        AddChildrenToDictionary();
    }

    private void AddChildrenToDictionary()
    {
        childTiles = GetComponentsInChildren<Tile>();
        tiles = new Dictionary<Vector2, Tile>();

        foreach (Tile child in childTiles)
        {
            print((int) child.transform.position.x);
            print((int) child.transform.position.y);
            tiles[new Vector2((int) child.transform.position.x, (int) child.transform.position.y)] = child;
        }
    }

    public Tile GetTileAtPos(Vector2 position)
    {
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        } else
        {
            print("return null");
            return null;
        }
    }


}
