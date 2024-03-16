using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour 
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Stack<Tile>> tiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        //each position corresponds to a stack of tiles at that position
        tiles = new Dictionary<Vector2, Stack<Tile>>();
        // foreach (KeyValuePair<Vector2, Stack<Tile>> entry in tiles) 
        // {
        //     entry.value = new Stack<Tile>();
        // }

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

                tiles[new Vector2(x,y)] = new Stack<Tile>();
                tiles[new Vector2(x,y)].Push(spawnedTile);

            }
        }


        cam.transform.position = new Vector3((float) width/2 - 0.5f, (float) height/2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 position)
    {
        if (tiles.TryGetValue(position, out Stack<Tile> stack))
        {
            return stack.Peek();
        } else
        {
            return null;
        }
    }
}