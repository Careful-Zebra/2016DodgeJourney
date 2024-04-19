using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MapHolder : MonoBehaviour {
    private Dictionary<Vector2, Tile> tiles;
    
    private Tile[] childTiles;

    private void Awake() {
        AddChildrenToDictionary();
    }

    private void AddChildrenToDictionary() {
        childTiles = GetComponentsInChildren<Tile>();
        tiles = new Dictionary<Vector2, Tile>();

        foreach (Tile child in childTiles) {
            /*print((int) child.transform.position.x);
            print((int) child.transform.position.y);*/
/*            Color childSpriteColor = child.GetComponent<SpriteRenderer>().color;
*//*            childSpriteColor.a = 0.5f;*//*
            child.GetComponent<SpriteRenderer>().color = childSpriteColor;*/
            tiles[new Vector2((int) Math.Floor(child.transform.position.x), (int) Math.Floor(child.transform.position.y))] = child;
        }
    }

    #region Tile Position Functions
    public Tile GetTileFromGeneralPos(Vector3 pos) {
        Vector2 pos2d = new Vector2((int)Math.Floor(pos.x), (int)Math.Floor(pos.y));
        return GetTileAtPos(pos2d);
    }

    public Tile GetTileAtPos(Vector2 position) {
        if (tiles.TryGetValue(position, out Tile tile)) {
            return tile;
        } else {
            print("return null");
            return null;
        }
    }

    #endregion
}
