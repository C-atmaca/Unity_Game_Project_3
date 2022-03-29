using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Vector3 spawnPosition = new Vector3(25, 0, 0);
    private float startDelay = 2;
    private float repeatRate = 2;

    private PlayerController playerCOntrollerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Reach the PlayerController script
        playerCOntrollerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Call SpawnObstacle function with a start delay and repeat it
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        // Keep spawning obstaclePrefab until gameOver is true
        if (playerCOntrollerScript.gameOver == false)
        {
            int randObstacle = Random.Range(0, 4);
            Instantiate(obstaclePrefabs[randObstacle], spawnPosition, obstaclePrefabs[randObstacle].transform.rotation);
        }
    }
}
