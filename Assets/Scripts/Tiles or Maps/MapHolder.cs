using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MapHolder : MonoBehaviour
{

    #region Editor Variables
    
    [SerializeField]
    [Tooltip("houses and offices, int is direction")]
    private List<Tile> locations;

    [SerializeField]
    [Tooltip("directions for the above MUST BE IN PERFECT ORDER")]
    private List<int> directions;

    [SerializeField]
    [Tooltip("Car prefab")]
    private GameObject car;
    #endregion
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
            /*print((int) child.transform.position.x);
            print((int) child.transform.position.y);*/
/*            Color childSpriteColor = child.GetComponent<SpriteRenderer>().color;
*//*            childSpriteColor.a = 0.5f;*//*
            child.GetComponent<SpriteRenderer>().color = childSpriteColor;*/
            tiles[new Vector2((int) Math.Floor(child.transform.position.x), (int) Math.Floor(child.transform.position.y))] = child;
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

    void Update() {
        int spawnChance = UnityEngine.Random.Range(0, 10);
        if (spawnChance ==7) {
            int count = locations.Count;
            int spawnIndex = UnityEngine.Random.Range(0, count);
            int endIndex = spawnIndex;
            while (endIndex == spawnIndex) {
                endIndex = UnityEngine.Random.Range(0, count);
            }
            Tile spawnTile = locations[spawnIndex];
            int dir = directions[spawnIndex];
            Tile destination = locations[endIndex];
            spawnTile.spawnCar(car, dir, destination, this);
        }
    }


}
