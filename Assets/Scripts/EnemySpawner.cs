
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public Transform player;

    [Header("Moving Enemy")]
    public GameObject movingEnemyPrefab;
    public int maxMovingEnemies = 2;
    public float movingSpawnMinTime = 3f;
    public float movingSpawnMaxTime = 6f;
    public float movingGroundY = 0f;

    [Header("Shooting Enemy")]
    public GameObject shootingEnemyPrefab;
    public int maxShootingEnemies = 1;
    public float shootingSpawnMinTime = 10f;
    public float shootingSpawnMaxTime = 15f;
    public float shootingSpawnY = 2f;

    [Header("Spawn Position")]
    [Range(1f, 1.5f)]
    public float spawnViewportX = 1.1f;

    [Header("Score Difficulty")]
    public int scorePerDifficultyLevel = 500;
    public int additionalEnemiesPerLevel = 1;
    public int maxAdditionalEnemies = 4;

    private float movingSpawnTimer;
    private float shootingSpawnTimer;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        ResetMovingTimer();
        ResetShootingTimer();
    }

    void Update()
    {
        HandleMovingEnemies();
        HandleShootingEnemies();
    }

    int GetAdditionalEnemies(int enemyType) // 0 = moving, 1 = shooting
    {
        if (ScoreManager.Instance == null || scorePerDifficultyLevel <= 0)
            return 0;

        int difficultyLevel =
            ScoreManager.Instance.Score / scorePerDifficultyLevel;
        
        if (enemyType == 0)
            return Mathf.Min(
                difficultyLevel * additionalEnemiesPerLevel,
                maxAdditionalEnemies
            );
        else
            return Mathf.Min(
                difficultyLevel * additionalEnemiesPerLevel,
                maxAdditionalEnemies
            )/2;
    }

    void HandleMovingEnemies()
    {
        if (movingEnemyPrefab == null)
            return;

        int currentMaxEnemies =
            maxMovingEnemies + GetAdditionalEnemies(0);

        if (currentMaxEnemies <= 0)
            return;

        movingSpawnTimer -= Time.deltaTime;

        if (movingSpawnTimer <= 0f)
        {
            int currentCount = FindObjectsByType<MovingEnemy>().Length;

            if (currentCount < currentMaxEnemies)
            {
                SpawnMovingEnemy();
                ResetMovingTimer();
            }
        }
    }

    void HandleShootingEnemies()
    {
        if (shootingEnemyPrefab == null)
            return;

        int currentMaxEnemies =
            maxShootingEnemies + GetAdditionalEnemies(1);

        if (currentMaxEnemies <= 0)
            return;

        shootingSpawnTimer -= Time.deltaTime;

        if (shootingSpawnTimer <= 0f)
        {
            int currentCount = FindObjectsByType<ShootingEnemy>().Length;

            if (currentCount < currentMaxEnemies)
            {
                SpawnShootingEnemy();
                ResetShootingTimer();
            }
        }
    }

    void SpawnMovingEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition(movingGroundY);

        GameObject enemy = Instantiate(
            movingEnemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        MovingEnemy movement = enemy.GetComponent<MovingEnemy>();

        if (movement != null)
            movement.groundY = movingGroundY;
    }

    void SpawnShootingEnemy()
    {
        Vector3 spawnPosition = GetSpawnPosition(shootingSpawnY);

        GameObject enemy = Instantiate(
            shootingEnemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        ShootingEnemy shooting = enemy.GetComponent<ShootingEnemy>();

        if (shooting != null)
            shooting.player = player;
    }

    Vector3 GetSpawnPosition(float spawnY)
    {
        if (mainCamera == null)
            return new Vector3(15f, spawnY, 0f);

        float cameraDistance =
            Mathf.Abs(mainCamera.transform.position.z);

        float spawnX = mainCamera.ViewportToWorldPoint(
            new Vector3(spawnViewportX, 0.5f, cameraDistance)
        ).x;

        return new Vector3(spawnX, spawnY, 0f);
    }

    void ResetMovingTimer()
    {
        movingSpawnTimer = Random.Range(
            movingSpawnMinTime,
            movingSpawnMaxTime
        );
    }

    void ResetShootingTimer()
    {
        shootingSpawnTimer = Random.Range(
            shootingSpawnMinTime,
            shootingSpawnMaxTime
        );
    }
}
