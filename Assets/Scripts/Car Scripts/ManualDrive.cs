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
                        if (transform.position.y < (curTile.gameObject.transform.position.y + 0.1) && 
                            transform.position.y > (curTile.gameObject.transform.position.y - 0.1))
                        {
                            Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                            transform.rotation = transform.rotation * leftTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    } else
                    {
                        if (transform.position.x < (curTile.gameObject.transform.position.x  + 0.1) && 
                            transform.position.x > (curTile.gameObject.transform.position.x - 0.1))
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
                        if (transform.position.y < (curTile.gameObject.transform.position.y + 0.1) &&
                            transform.position.y > (curTile.gameObject.transform.position.y - 0.1))
                        {
                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            upDown = !upDown;
                        }
                    }
                    else
                    {
                        if (transform.position.x < (curTile.gameObject.transform.position.x + 0.1) &&
                            transform.position.x > (curTile.gameObject.transform.position.x - 0.1))
                        {

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

            Vector2 tempVelocity = transform.right * speed;

            rb.velocity = HandleTraffic(tempVelocity, curTile);

            

        } else
        {
            //flip the car around at the end.
            Quaternion oneEighty = Quaternion.Euler(0, 0, -180);
            transform.rotation = transform.rotation * oneEighty;
            GameObject[] temp = lTurns;
            lTurns = rTurns;
            rTurns = temp;
            rb.velocity = transform.right * speed;
        }
    }

    private Vector2 HandleTraffic(Vector2 tempVelocity, Tile curTile)
    {
        if (!curTile.HasTrafficObj())
        {
            return tempVelocity;
        } else
        {
            if (curTile.TrafficObj() == "Traffic Light")
            {
                /*print(curTile.TrafficGO().GetComponent<TrafficLightManager>().GetState());*/
                if (curTile.TrafficGO().GetComponent<TrafficLightManager>().GetState() == 0)
                {
                    return Vector2.zero;
                }
            }
        }
        return tempVelocity;
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

    #region A*
    private void aStar(Tile destination)
    {
        

        ArrayList visited = new ArrayList();
        Tile currentTile = TileCarIsIn();
        Dictionary<float, Tile> neighbours = new Dictionary<float, Tile>();
        float tileSize = currentTile.GetComponent<SpriteRenderer>().bounds.max.x - currentTile.GetComponent<SpriteRenderer>().bounds.min.x;
        visited.Add(currentTile);

        //get the values off of the dest
        int destX = (int)destination.transform.position.x;
        int destY = (int)destination.transform.position.y;

        //add all of the neighbours of the starting tile
        int xPos = (int)Math.Floor(transform.position.x);
        int yPos = (int)Math.Floor(transform.position.y);
        Vector2 belowPos = new Vector2(xPos, yPos - tileSize);
        Tile tileBelow = mapHolder.GetTileAtPos(belowPos);
        if (tileBelow.Driveable())
        {
            neighbours.Add(1 + heuristic(tileBelow, destination), tileBelow);
        }
        
        Tile tileAbove = mapHolder.GetTileAtPos(new Vector2(xPos, yPos + tileSize));
        if (tileAbove.Driveable())
        {
            neighbours.Add(1 + heuristic(tileAbove, destination), tileAbove);
        }
        Tile tileLeft = mapHolder.GetTileAtPos(new Vector2(xPos - tileSize, yPos));
        if (tileLeft.Driveable())
        {
            neighbours.Add(1 + heuristic(tileLeft, destination), tileLeft);
        }
        Tile tileRight = mapHolder.GetTileAtPos(new Vector2(xPos + tileSize, yPos));

        //main loop
        

    }

    //straight line distance to the destination
    private float heuristic(Tile current, Tile destination)
    {
        float curX = current.transform.position.x;
        float curY = current.transform.position.y;
        float destX = destination.transform.position.x;
        float destY = destination.transform.position.y;
        float diffX = Math.Abs(curX - destX);
        float diffY = Math.Abs(curY - destY);
        return (float)Math.Sqrt(diffX + diffY);
    }
    #endregion
}
