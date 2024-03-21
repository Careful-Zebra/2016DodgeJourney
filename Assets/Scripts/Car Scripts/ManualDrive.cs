using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ManualDrive : MonoBehaviour {
    [SerializeField]
    private float speed;

    [SerializeField]
    [Tooltip("The map object that holds all the tiles")]
    private MapHolder mapHolder;

    [SerializeField]
    [Tooltip("The left turns the car needs to make")]
    private GameObject[] lTurns;

    [SerializeField]
    [Tooltip("The right turns the car needs to make")]
    private GameObject[] rTurns;

    [SerializeField]
    [Tooltip("The starting orientation of the car (true for up/down, false for sideysidey)")]
    private Boolean upDown;

    #region private variables
    //whether the car has just rotated or not
    private Boolean justTurned;
    #endregion


    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        justTurned = false;
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
            /*print("Current transform.rotation: " + transform.rotation.eulerAngles.ToString());*/

            /*if (lTurns.Contains(curTile.gameObject))
            {
                
                //if the car is now starting to turn, then we can set the target rotation
                if (!justTurned)
                {
                    Quaternion leftTurn = Quaternion.Euler(0, 90, 0);
                    target = transform.rotation * leftTurn;
                }

                justTurned = true;

                transform.rotation = Quaternion.RotateTowards(transform.rotation, target)

            } else if (rTurns.Contains(curTile.gameObject))
            {
                transform.Rotate(transform.forward, -85.0f * t, Space.World);
            } else
            {
                justTurned = false;
            }*/


            //if the tile we are in is a left turning one
            if (lTurns.Contains(curTile.gameObject))
            {
                if (!justTurned)
                {
                    if (upDown)
                    {
                        //TRY IT WITH PLUSES
                        if (transform.position.y < (curTile.gameObject.transform.position.y - 0.6) && 
                            transform.position.y > (curTile.gameObject.transform.position.y - 0.4))
                        {
                            Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                            transform.rotation = transform.rotation * leftTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    } else
                    {
                        if (transform.position.x < (curTile.gameObject.transform.position.x  + 0.6) && 
                            transform.position.x > (curTile.gameObject.transform.position.x + 0.4))
                        {
                            Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                            transform.rotation = transform.rotation * leftTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    }
                }      
            //if the tile we are in is a right turning one
            } else if (rTurns.Contains(curTile.gameObject))
            {
                if (!justTurned)
                {
                    if (upDown)
                    {
                        if (transform.position.y < (curTile.gameObject.transform.position.y - 0.6) &&
                            transform.position.y > (curTile.gameObject.transform.position.y - 0.4))
                        {
                            print("TURNING");
                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    }
                    else
                    {
                        if (transform.position.x < (curTile.gameObject.transform.position.x + 0.6) &&
                            transform.position.x > (curTile.gameObject.transform.position.x + 0.4))
                        {
                            print("TURNING");

                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    }
                }
            } else
            {
                justTurned = false;
            }

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
/*        print(xPos);
        print(yPos);*/
        Tile currentTile = mapHolder.GetTileAtPos(new Vector2(xPos, yPos));
        /*print(currentTile);*/
        return currentTile;
    }


    #endregion
}
