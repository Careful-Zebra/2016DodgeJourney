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

    private MapHolder mapHolder;  // Script that contains the tiles
    private LevelInfo levelInfo;  // Script that contains level info

    private string destinationTag;  // Determines what type of building the car spawned here should go to
    private float spawnTime;  // How much time left before spawning another car is allowed

    private void Awake() {
        GameObject mapHolderObject = GameObject.Find("MapHolder");
        levelInfo = mapHolderObject.GetComponent<LevelInfo>();
        mapHolder = mapHolderObject.GetComponent<MapHolder>();

        if (gameObject.tag == "House") {
            destinationTag = "Office";
        } else {
            destinationTag = "House";
        }
    }

    void Update() {
        TrySpawnCar();
    }

    /* Attempts to spawn a car. Fails if spawnCooldown not over or failed probability. Success, then spawn a car at building's location. */
    void TrySpawnCar() {
        if (spawnTime > 0f) {
            spawnTime -= Time.deltaTime;
            return;
        }
        if (levelInfo.ProbabilisticallySpawnCar()) {
            GameObject newCar = Instantiate(car, transform.position, Quaternion.identity);
            //newCar.destinationTile = GetRandomDestinationTile();
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
