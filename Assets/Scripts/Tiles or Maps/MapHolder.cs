using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MapHolder : MonoBehaviour {

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

    [SerializeField]
    [Tooltip("stores references to necessary prefabs")]
    private PrefabManager pfabber;
    #endregion

    private Dictionary<Vector2, Tile> tiles;
    
    private Tile[] childTiles;

    private void Awake() {
        AddChildrenToDictionary();
    }

    private void AddChildrenToDictionary() {
        childTiles = GetComponentsInChildren<Tile>();
        tiles = new Dictionary<Vector2, Tile>();

        foreach (Tile child in childTiles) { // runtime processing/updating for each tile
            if (!child.Driveable())
            {
                Color desiredColor = new Color();
                desiredColor.r = 224f/255f;
                desiredColor.g = 216f/255f;
                desiredColor.b = 255f/255f;
                desiredColor.a = 1f;

                Color desiredColor2 = Color.yellow;
                desiredColor2.a = 1f;
                child.gameObject.GetComponent<SpriteRenderer>().color = desiredColor;
            }

            child.SetStopSignPrefab(pfabber.GiveMeAStopSignPrefab()); 
            // one liner that accesses 
        

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
    
    // void Update() {
    //    int spawnChance = UnityEngine.Random.Range(0, 20);
    //    if (spawnChance ==7) {
    //        int count = locations.Count;
    //        int spawnIndex = UnityEngine.Random.Range(0, count);
    //        int endIndex = spawnIndex;
    //        while (endIndex == spawnIndex) {
    //            endIndex = UnityEngine.Random.Range(0, count);
    //        }
    //        Tile spawnTile = locations[spawnIndex];
    //        int dir = directions[spawnIndex];
    //        Tile destination = locations[endIndex];
    //        spawnTile.spawnCar(car, dir, destination, this);
    //    }

    //    //childTiles = GetComponentsInChildren<Tile>();
    // //    foreach (Tile child in childTiles) {
    // //     if (child.Driveable()) {
    // //         child.GetComponent<SpriteRenderer>().material.color = Color.black;
    // //     }
    // //    }
    // }


}
