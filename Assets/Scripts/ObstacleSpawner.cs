using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstacles;

    public float minSpawnTime = 1.5f;
    public float maxSpawnTime = 3f;

    private float timer;

    void Start()
    {
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GameObject newObstacle = SpawnObstacle();

            float width = newObstacle.GetComponent<SpriteRenderer>().bounds.size.x;
            float speed = newObstacle.GetComponent<ObstacleMovement>().speed;

            timer = Random.Range(minSpawnTime, maxSpawnTime) + width / speed;
        }
    }

    private GameObject SpawnObstacle()
    {
        int index = Random.Range(0, obstacles.Length);
        GameObject obstacle = obstacles[index];

        Vector3 spawnPosition = new Vector3(transform.position.x, obstacle.transform.position.y, 0);

        return Instantiate(obstacle, spawnPosition, Quaternion.identity);
    }
}
