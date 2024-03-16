using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour 
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                bool isOffset = ((x % 2 == 0 && y % 2 != 0) | (x % 2 != 0 && y % 2 == 0));
                // if (isOffset)
                // {
                //     spawnedTile.GetComponent<SpriteRenderer>.color = 
                // }
                spawnedTile.Init(isOffset);

                tiles[new Vector2(x,y)] = spawnedTile;

            }
        }


        cam.transform.position = new Vector3((float) width/2 - 0.5f, (float) height/2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position, out Tile tile))
        {
            return tile;
        } else
        {
            return null;
        }
    }
}
