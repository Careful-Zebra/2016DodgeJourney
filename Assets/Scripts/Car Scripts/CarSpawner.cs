using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script should be attatched to Buildings (House and Office).
* When this script is working, it will check a publicly available list of buildings (either a list of Houses
* or a list of Offices). Then, it will randomly select one of the buildings to go to and plot the course for
* the car.
*/
public class CarSpawner : MonoBehaviour {

    [SerializeField] private GameObject car;
    [SerializeField] private float spawnCooldown;  // How long building should wait before spawning another car

    // [SerializeField]
    // [Tooltip("The object that holds references to the CarController scripts of all cars")]
    // private CarHolder carHolder;

    private MapHolder mapHolder;  // Script that contains the tiles
    private LevelInfo levelInfo;  // Script that contains level info

    private string destinationTag;  // Determines what type of building the car spawned here should go to
    private float spawnTime;  // How much time left before spawning another car is allowed
    private int facingDirection;  // Direction building is facing. 0 North, 1 East, 2 South, 3 West

    private void Start() { // this seemingly needs to be start rather than OnEnable, as levelinfo needs to populate the buildings dictionary with the two keys
        GameObject mapHolderObject = GameObject.Find("MapHolder");
        mapHolder = mapHolderObject.GetComponent<MapHolder>();
        levelInfo = mapHolderObject.GetComponent<LevelInfo>();
        spawnTime += Random.Range(0f, spawnCooldown);

        Debug.Log(gameObject.tag);
        // List<GameObject> building_list;
        // // check if the dictionary of buildings has a list of homes/offices, depending on whether the object this script is attached to is a house or office
        // bool exists = levelInfo.buildings.TryGetValue(gameObject.tag, out building_list);
        // if (!exists) {
        //     levelInfo.buildings[gameObject.tag] = new List<GameObject>();
        // }
        levelInfo.buildings[gameObject.tag].Add(gameObject);  // Add self to list of building type

        if (gameObject.tag == "House") {
            destinationTag = "Office";
        } else {
            destinationTag = "House";
        }

        // facingDirection = ((Mathf.CeilToInt(transform.position.z + 180f) / 90) - 1) % 4;  // Orientation of 2D objects is determined by z
        facingDirection = 1;
    }

    void Update() {
        TrySpawnCar();
    }

    /* Attempts to spawn a car. Fails if spawnCooldown not over or failed probability. Success, then spawn a car at building's location. */
    void TrySpawnCar() {
        if (spawnTime > 0f) {
            spawnTime -= Time.deltaTime;
            spawnCooldown -= Time.deltaTime * 0.25f;
            return;
        }
        if (levelInfo.ProbabilisticallySpawnCar()) {
            GameObject newCar = Instantiate(car, transform.position, Quaternion.Euler(0, 0, 0));
            ManualDrive newCarManualDrive = newCar.GetComponent<ManualDrive>();
            // Initialize newCar's parameters.
            newCarManualDrive.Destination = GetRandomDestinationTile();
            newCarManualDrive.MapHolder = mapHolder;
            newCarManualDrive.direction = facingDirection;
            //carHolder.AddCarToSet(car.GetComponent<CarController>());
            // Debug.Log("Initializing new car");

            spawnTime = spawnCooldown;
        }
    }

    private Tile GetRandomDestinationTile() {
        List<GameObject> destinationList = (levelInfo.buildings)[destinationTag];
        int randIndex = Random.Range(0, destinationList.Count);
        Vector3 destPos = destinationList[randIndex].transform.position;
        Tile destTile = mapHolder.GetTileFromGeneralPos(destPos);
        return destTile;
    }
}
