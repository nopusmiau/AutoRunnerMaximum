using UnityEngine;
using UnityEngine.InputSystem;

// Put this on an empty GameObject on the right side of the screen (outside the camera view).
// After space is pressed it spawns a random obstacle every few seconds.
public class ObstacleSpawner : MonoBehaviour
{
    // drag the obstacle prefabs in here from the Inspector
    public GameObject[] obstacles;

    // random time between two obstacles (in seconds)
    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;

    private float timer;
    private bool isSpawning = false;

    void Start()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        // start spawning when space is pressed, same as the background
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            isSpawning = true;

        if (!isSpawning)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnObstacle();
            timer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnObstacle()
    {
        // pick a random obstacle from the list
        int index = Random.Range(0, obstacles.Length);
        GameObject obstacle = obstacles[index];

        // x comes from the spawner, y comes from the prefab
        // (so rocks stay on the ground and the floating ledge stays in the air)
        Vector3 spawnPosition = new Vector3(transform.position.x, obstacle.transform.position.y, 0);

        Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
