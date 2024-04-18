using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
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
    private ArrayList lTurns;

    [SerializeField]
    [Tooltip("The right turns the car needs to make")]
    private ArrayList rTurns;

    [SerializeField]
    [Tooltip("The starting direction of the car 0 north, 1 east, etc")]
    private int dir;

    [SerializeField]
    [Tooltip("The destination tile")]
    private Tile destination;



    #region private variables
    //whether the car has just rotated or not
    private Boolean justTurned;
    private float actualSpeed;

    //yeah sorry
    private int stopSignCount = 0;
    #endregion


    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        justTurned = false;
    }

    // Start is called before the first frame update
    void Start() {

        actualSpeed = speed;

        lTurns = new ArrayList();
        rTurns = new ArrayList();

        //setup the left and right turns
        Tile[] path = aStar(destination);

        int tempDir = dir;
        for (int i = 0; i < path.Length - 1; i++)
        {
            if (path[i].transform.position.x < path[i + 1].transform.position.x)
            {
                if (tempDir == 0)
                {
                    rTurns.Add(path[i]);
                } else if (tempDir == 2)
                {
                    lTurns.Add(path[i]);
                }
                tempDir = 1;
            } else if (path[i].transform.position.x > path[i + 1].transform.position.x)
            {
                if (tempDir == 0)
                {
                    lTurns.Add(path[i]);
                }
                else if (tempDir == 2)
                {
                    rTurns.Add(path[i]);
                }
                tempDir = 3;
            } else if (path[i].transform.position.y > path[i + 1].transform.position.y)
            {
                if (tempDir == 1)
                {
                    rTurns.Add(path[i]);
                } else if (tempDir == 3)
                {
                    lTurns.Add(path[i]);
                }
                tempDir = 2;
            } else
            {
                if (tempDir == 1)
                {
                    lTurns.Add(path[i]);
                }
                else if (tempDir == 3)
                {
                    rTurns.Add(path[i]);
                }
                tempDir = 0;
            }
        }
        
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
            if (lTurns.Contains(curTile))
            {

                if (!justTurned)
                {
                    if (dir == 0)
                    {

                        if (transform.position.y > (curTile.gameObject.transform.position.y - 0.4) &&
                            transform.position.y < (curTile.gameObject.transform.position.y - 0.2))
                        {
                            Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                            transform.rotation = transform.rotation * leftTurn;
                            justTurned = true;

                            dir = 3;

                        }
                    }
                    else if (dir == 2) {
                        if (transform.position.y < (curTile.gameObject.transform.position.y + 0.4) &&
                            transform.position.y > (curTile.gameObject.transform.position.y + 0.2))
                        {
                            Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                            transform.rotation = transform.rotation * leftTurn;
                            justTurned = true;

                            dir = 1;

                        }
                    }
                    else
                    {
                        if (dir == 1)
                        {
                            if (transform.position.x > (curTile.gameObject.transform.position.x - 0.4) &&
                            transform.position.x < (curTile.gameObject.transform.position.x - 0.2))
                            {
                                Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                                transform.rotation = transform.rotation * leftTurn;
                                justTurned = true;
                                dir = 0;
                            }
                        } else
                        {
                            if (transform.position.x < (curTile.gameObject.transform.position.x + 0.4) &&
                            transform.position.x > (curTile.gameObject.transform.position.x + 0.2))
                            {
                                Quaternion leftTurn = Quaternion.Euler(0, 0, 90);
                                transform.rotation = transform.rotation * leftTurn;
                                justTurned = true;
                                dir = 2;
                            }
                        }
                        
                    }
                }      
            //if the tile we are in is a right turning one
            } else if (rTurns.Contains(curTile))
            {
                if (!justTurned)
                {
                    if (dir == 0)
                    {
                        if (transform.position.y < (curTile.gameObject.transform.position.y + 0.4) &&
                            transform.position.y > (curTile.gameObject.transform.position.y + 0.2))
                        {
                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            dir = 1;
                        }
                    } else if (dir == 2)
                    {
                        if (transform.position.y > (curTile.gameObject.transform.position.y - 0.4) &&
                            transform.position.y < (curTile.gameObject.transform.position.y - 0.2))
                        {
                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            dir = 3;
                        }
                    }
                    else if (dir == 1) 
                    {
                        if (transform.position.x < (curTile.gameObject.transform.position.x + 0.4) &&
                            transform.position.x > (curTile.gameObject.transform.position.x + 0.2))
                        {

                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            dir = 2;
                        }
                    } else
                    {
                        if (transform.position.x > (curTile.gameObject.transform.position.x - 0.4) &&
                            transform.position.x < (curTile.gameObject.transform.position.x - 0.2))
                        {

                            Quaternion rightTurn = Quaternion.Euler(0, 0, -90);
                            transform.rotation = transform.rotation * rightTurn;
                            justTurned = true;
                            dir = 0;
                        }
                    }
                }
            }
            else if (curTile == destination)
            {
                //flip the car around at the end.
                Quaternion oneEighty = Quaternion.Euler(0, 0, -180);
                transform.rotation = transform.rotation * oneEighty;
                ArrayList temp = lTurns;
                lTurns = rTurns;
                rTurns = temp;
                rb.velocity = transform.right * speed;
                justTurned = false;
                dir = (dir + 2) % 4;
            }
            else
            {
                justTurned = false;
            }

            if (dir == 0)
            {

            }

            Vector2 tempVelocity = transform.right * actualSpeed;

            rb.velocity = HandleTraffic(tempVelocity, curTile);
            rb.velocity = checkAhead(rb.velocity);
            

        }
        else
        {
            //flip the car around at the end.
            Quaternion oneEighty = Quaternion.Euler(0, 0, -180);
            transform.rotation = transform.rotation * oneEighty;
            ArrayList temp = lTurns;
            lTurns = rTurns;
            rTurns = temp;
            rb.velocity = transform.right * speed;
            dir = (dir + 2) % 4;
            justTurned = false;
        }

        

    }


    //Use a raycast in order to check if there are cars in front of us, and if so stop
    private Vector2 checkAhead(Vector2 tempVelocity) {

        int layerMask = 1 << 9;
        float dist = 1;
        Vector2 pos = transform.GetChild(0).position;
        RaycastHit2D hit = Physics2D.Raycast(pos, transform.right, dist, layerMask);
        // Debug.DrawRay(pos, transform.right * dist, Color.red, 10.0f);
        Vector2 velocity = tempVelocity;
        // Debug.DrawRay(transform.position, transform.right, Color.red, layerMask);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Car"))
        {
            velocity = Vector2.zero;
        }

        return velocity;
    }

    private Vector2 HandleTraffic(Vector2 tempVelocity, Tile curTile)
    {
        if (!curTile.HasTrafficObj())
        {
            stopSignCount = 0;
            return tempVelocity;
        } else
        {
            if (curTile.TrafficObj() == "Traffic Light")
            {
                /*print(curTile.TrafficGO().GetComponent<TrafficLightManager>().GetState());*/
                if (curTile.TrafficGO().GetComponent<TrafficLightManager>().GetState(dir) == 0)
                {
                    return Vector2.zero;
                }
            } else if (curTile.TrafficObj() == "Stop Sign") {
                if (!curTile.TrafficGO().GetComponent<StopSignManager>().CanIGo(gameObject)) {
                    return Vector2.zero;
                } else if (stopSignCount < 100) {
                    stopSignCount++;
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

    #region Public Methods
    public Tile curTile() {
        return TileCarIsIn();
    }
    #endregion

    #region A*
    private Tile[] aStar(Tile destination)
    {
        

        ArrayList visited = new ArrayList();
        Tile currentTile = TileCarIsIn();
        Dictionary<Tile, float> neighbours = new Dictionary<Tile, float>();
        float tileSize = currentTile.GetComponent<SpriteRenderer>().bounds.max.x - currentTile.GetComponent<SpriteRenderer>().bounds.min.x;
        visited.Add(currentTile);
        Dictionary<Tile, Tile> prev = new Dictionary<Tile, Tile>();



        //add all of the neighbours of the starting tile
        int xPos = (int)Math.Floor(transform.position.x);
        int yPos = (int)Math.Floor(transform.position.y);
        Vector2 belowPos = new Vector2(xPos, yPos - tileSize);
        Tile tileBelow = mapHolder.GetTileAtPos(belowPos);
        if (tileBelow.Driveable() | tileBelow == destination)
        {
            neighbours.Add(tileBelow, 1 + heuristic(tileBelow, destination));
            prev.Add(tileBelow, currentTile);
        }
        
        Tile tileAbove = mapHolder.GetTileAtPos(new Vector2(xPos, yPos + tileSize));
        if (tileAbove.Driveable() | tileAbove == destination)
        {
            neighbours.Add(tileAbove, 1 + heuristic(tileAbove, destination));
            prev.Add(tileAbove, currentTile);
        }
        Tile tileLeft = mapHolder.GetTileAtPos(new Vector2(xPos - tileSize, yPos));
        if (tileLeft.Driveable() | tileLeft == destination)
        {
            neighbours.Add(tileLeft, 1 + heuristic(tileLeft, destination));
            prev.Add(tileLeft, currentTile);
        }
        Tile tileRight = mapHolder.GetTileAtPos(new Vector2(xPos + tileSize, yPos));
        if (tileRight.Driveable() | tileRight == destination)
        {
            neighbours.Add(tileRight, 1 + heuristic(tileRight, destination));
            prev.Add(tileRight, currentTile);
        }

        //main loop

        Tile minTile = currentTile;
        float minDist = int.MaxValue;
        foreach (KeyValuePair<Tile, float> pair in neighbours)
        {
            if (pair.Value < minDist)
            {
                minTile = pair.Key;
                minDist = pair.Value;
            }
        }
        currentTile = minTile;
        float dist = 1;
        neighbours.Remove(currentTile);
        visited.Add(currentTile);
        while (currentTile != destination)
        {
            xPos = (int)Math.Floor(currentTile.transform.position.x);
            yPos = (int)Math.Floor(currentTile.transform.position.y);
            tileBelow = mapHolder.GetTileAtPos(new Vector2(xPos, (float)(yPos - tileSize)));
            if (tileBelow != null && !visited.Contains(tileBelow) && (tileBelow.Driveable() | tileBelow == destination) && 
                (!neighbours.ContainsKey(tileBelow) | neighbours.GetValueOrDefault(tileBelow) < dist + heuristic(tileBelow, destination)))
            {
                if (!neighbours.TryAdd(tileBelow, dist + heuristic(tileBelow, destination)))
                {
                    neighbours.Remove(tileBelow);
                    neighbours.Add(tileBelow, dist + heuristic(tileBelow, destination));
                }

                if (!prev.TryAdd(tileBelow, currentTile))
                {
                    prev.Remove(tileBelow);
                    prev.Add(tileBelow, currentTile);
                }
            }

            tileAbove = mapHolder.GetTileAtPos(new Vector2(xPos, yPos + tileSize));
            if (tileAbove != null && !visited.Contains(tileAbove) && (tileAbove.Driveable() | tileAbove == destination) && 
                (!neighbours.ContainsKey(tileAbove) | neighbours.GetValueOrDefault(tileAbove) < dist + heuristic(tileAbove, destination)))
            {
                if (!neighbours.TryAdd(tileAbove, dist + heuristic(tileAbove, destination)))
                {
                    neighbours.Remove(tileAbove);
                    neighbours.Add(tileAbove, dist + heuristic(tileAbove, destination));
                }

                if (!prev.TryAdd(tileAbove, currentTile))
                {
                    prev.Remove(tileAbove);
                    prev.Add(tileAbove, currentTile);
                }
            }
            tileLeft = mapHolder.GetTileAtPos(new Vector2(xPos - tileSize, yPos));
            if (tileLeft != null && !visited.Contains(tileLeft) && (tileLeft.Driveable() | tileLeft == destination) && 
                (!neighbours.ContainsKey(tileLeft) | neighbours.GetValueOrDefault(tileLeft) < dist + heuristic(tileLeft, destination)))
            {
                if (!neighbours.TryAdd(tileLeft, dist + heuristic(tileLeft, destination)))
                {
                    neighbours.Remove(tileLeft);
                    neighbours.Add(tileLeft, dist + heuristic(tileLeft, destination));
                }

                if (!prev.TryAdd(tileLeft, currentTile))
                {
                    prev.Remove(tileLeft);
                    prev.Add(tileLeft, currentTile);
                }
            }
            tileRight = mapHolder.GetTileAtPos(new Vector2(xPos + tileSize, yPos));
            if (tileRight != null && !visited.Contains(tileRight) && (tileRight.Driveable() | tileRight == destination) && 
                (!neighbours.ContainsKey(tileRight) | neighbours.GetValueOrDefault(tileRight) < dist + heuristic(tileRight, destination)))
            {
                if (!neighbours.TryAdd(tileRight, dist + heuristic(tileRight, destination)))
                {
                    neighbours.Remove(tileRight);
                    neighbours.Add(tileRight, dist + heuristic(tileRight, destination));
                }
                
                if (!prev.TryAdd(tileRight, currentTile))
                {
                    prev.Remove(tileRight);
                    prev.Add(tileRight, currentTile);
                }
            }

            //update the values
            minTile = currentTile;
            minDist = int.MaxValue;
            foreach (KeyValuePair<Tile, float> pair in neighbours)
            {
                if (pair.Value < minDist)
                {
                    minTile = pair.Key;
                    minDist = pair.Value;
                }
            }
            currentTile = minTile;
            visited.Add(currentTile);
            dist =  1 + neighbours.GetValueOrDefault(currentTile) - heuristic(currentTile, destination);
            neighbours.Remove(currentTile);
            
            
        }


        //stack bc we are reversing thru the list
        Stack<Tile> path = new Stack<Tile>();

        while (currentTile != TileCarIsIn())
        {
            path.Push(currentTile);
            prev.TryGetValue(currentTile, out currentTile);
        }
        
        int len = path.Count;
        Tile[] returner = new Tile[len];
        for (int i = 0; i < len; i++)
        {
            returner[i] = path.Pop();
        }

        return returner;

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
        return (float)Math.Sqrt((diffX *diffX) + (diffY *diffY));
    }
    #endregion

    
}
