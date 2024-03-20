using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManualDrive : MonoBehaviour {
    [SerializeField]
    private float speed;

    [SerializeField]
    [Tooltip("The map object that holds all the tiles")]
    private MapHolder mapHolder;


    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        float t = Time.deltaTime;
        Tile curTile = TileCarIsIn();

        /* Commented out input forward drive code but left for later use if wanted
                if (Input.GetKey(KeyCode.W)) {
                    rb.velocity = transform.right * speed;
                } else if (Input.GetKey(KeyCode.S)) {
                    rb.velocity = transform.right * -speed;
                } else {
                    rb.velocity = Vector3.zero;
                }

                */

        if (Input.GetKey(KeyCode.A))
        {
            //Rotate the current game object's transform, using fixedDeltaTime for consistency across machines.
            transform.Rotate(transform.forward, 100.0f * t, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the current game object's transform, using fixedDeltaTime for consistency across machines.
            transform.Rotate(transform.forward, -100.0f * t, Space.World);

        }
        if (curTile.Driveable())
        {
            rb.velocity = transform.right * speed;
        } else
        {
            rb.velocity = Vector3.zero;
        }
    }

    #region Ella things (sorry)


    private Tile TileCarIsIn()
    {

        int xPos = (int) Math.Floor(transform.position.x);
        int yPos = (int) Math.Floor(transform.position.y);
        print(xPos);
        print(yPos);
        Tile currentTile = mapHolder.GetTileAtPos(new Vector2(xPos, yPos));
        print(currentTile.name);
        return currentTile;
    }


    #endregion
}
