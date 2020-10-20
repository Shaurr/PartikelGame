using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    private List<Vector3> objects = new List<Vector3>();

    private GameObject player;
    [SerializeField]
    private GameController gc;
    [SerializeField]
    private GameObject[] enemiesPrefab;
    [SerializeField]
    private GameObject[] itemsPrefab;
    private List<GameObject> createdItems = new List<GameObject>();
    private Vector3 playerPosition;
    private List<GameObject> createdEnemies = new List<GameObject>();

    // gets Enemies to spawn starts to spawn them
    public void SpawnEnemies(int[] enemies) {
        // get player position 
        playerPosition = player.transform.position;
        // clears lists
        createdEnemies.Clear();
        objects.Clear();
        // for each enemie call add with type
        for (int j = 0; j < 3; j++) {
            for (int i = enemies[j]; i > 0; i--) {
                Add(j);

            }
        }

    }

    // spawns enemies
    private void Add(int type) {
        // get spawnposition
        Vector3 position = SpawnPoint();
        
        if (position.Equals(new Vector3(50, 50, 50))) {
            // no free position, delete enemie
            gc.DeleteEnemie(type);
        }
        else {
            // spawn new enemie
            GameObject newEnemie = Instantiate(enemiesPrefab[type], position, Quaternion.identity);
            createdEnemies.Add(newEnemie);
            newEnemie.GetComponent<EnemieController>().Initialize();
        }
    }

    // searches for free spawnpoint
    public Vector3 SpawnPoint() {

        bool free = true;
        int i = 0;
        float x, y = 0;
        Vector3 newSpawn;

        do {
            // set random point
            x = Random.Range(-4.9f, 4.9f);
            y = Random.Range(-3.6f, 3.35f);
            free = true;
            i++;
            // check if overlap with player
            if ((x >= playerPosition.x - 1.5f && x <= playerPosition.x + 1.5f) && (y >= playerPosition.y - 1.5f && y <= playerPosition.y + 1.5f)) {
                free = false;

            }
            // check if overlap with other objects
            else {
                foreach (Vector3 obj in objects) {
                    if ((x >= obj.x - 0.6f && x <= obj.x + 0.6f) && (y >= obj.y - 0.6f && y <= obj.y + 0.6f)) {
                        free = false;
                        break;
                    }

                }
            }
            // when point not free repeat, max 20 times
        } while (!free && i < 20);

       
        if (i == 20 && !free) {
            // no free point
            newSpawn = new Vector3(50, 50, 50);
        }
        else {
            // set spawnpoint to free point
            newSpawn = new Vector3(x, y, 0);
            objects.Add(newSpawn);
        }

        // return spawnpoint
        return newSpawn;


    }

    // spawns items
    public void Spawn(int type) {
        // get player position
        playerPosition = player.transform.position;
        // get spawnpoint
        Vector3 position = SpawnPoint();
        if (position.Equals(new Vector3(50, 50, 50))) {
            // no free point
        }
        else {
            // spawn items
            GameObject newItem = Instantiate(itemsPrefab[type], position, Quaternion.identity);
            createdItems.Add(newItem);
        }

    }

    // deletes items (on death)
    public void DeleteItems() {

        foreach (GameObject item in createdItems) {

            Destroy(item);
        }

    }

    // delete enemies (on death)
    public void DeleteEnemies() {

        foreach (GameObject enemie in createdEnemies) {

            Destroy(enemie);
        }

    }

    // set player
    public void SetPlayer(GameObject _player) {

        player = _player;

    }
}
