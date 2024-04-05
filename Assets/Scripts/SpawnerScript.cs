using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    #region editor variablkes
    [SerializeField]
    [Tooltip("car prefab")]
    private GameObject[] cars;

    [SerializeField]
    [Tooltip("list of source tiles")]
    private Tile[] sources;


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 1000) == 3)
        {
            print("spawning");
            int index = Random.Range(0, sources.Length - 1);
            int cIndex = Random.Range(0, cars.Length - 1);
            GameObject spawned = Instantiate(cars[cIndex], sources[index].transform);
            spawned.SetActive(true);

        }
    }
}
