using UnityEngine;


public class PickupSpawner : MonoBehaviour
{
    [Header("Coins")]
    public GameObject coinPrefab;
    public float minCoinTime = 1.5f;
    public float maxCoinTime = 3f;
    [Tooltip("How many coins in each group.")]
    public int minCoinsPerGroup = 3;
    public int maxCoinsPerGroup = 6;
    [Tooltip("Gap between coins in a group (world units).")]
    public float coinSpacing = 0.8f;

    [Header("Powerups")]
    public GameObject[] powerUpPrefabs;
    public float minPowerUpTime = 8f;
    public float maxPowerUpTime = 15f;

    [Header("Spawn heights (world Y)")]
    [Tooltip("Lowest height: the player runs straight through these.")]
    public float lowY = -2.5f;
    [Tooltip("Highest height: the player has to jump for these.")]
    public float highY = 0.5f;

    private float coinTimer;
    private float powerUpTimer;

    void Start()
    {
        coinTimer = 0.5f;
        powerUpTimer = Random.Range(minPowerUpTime, maxPowerUpTime);
    }

    void Update()
    {
        coinTimer -= Time.deltaTime;
        if (coinTimer <= 0f)
        {
            SpawnCoinGroup();
            coinTimer = Random.Range(minCoinTime, maxCoinTime);
        }

        powerUpTimer -= Time.deltaTime;
        if (powerUpTimer <= 0f && powerUpPrefabs != null && powerUpPrefabs.Length > 0)
        {
            GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
            Spawn(prefab, transform.position.x + 2f, Random.Range(lowY, highY));
            powerUpTimer = Random.Range(minPowerUpTime, maxPowerUpTime);
        }
    }

    void SpawnCoinGroup()
    {
        if (coinPrefab == null) return;

        int count = Random.Range(minCoinsPerGroup, maxCoinsPerGroup + 1);
        float startX = transform.position.x;
        float height = highY - lowY;

        
        int shape = Random.Range(0, 5);
        float lineY = Random.value < 0.5f ? lowY : highY;

        for (int i = 0; i < count; i++)
        {
            float t = count > 1 ? (float)i / (count - 1) : 0.5f; // 0 at first coin, 1 at last
            float x = startX + i * coinSpacing;
            float y;

            switch (shape)
            {
                case 0: 
                    y = lineY;
                    break;
                case 1: 
                    y = lowY + Mathf.Sin(t * Mathf.PI) * height;
                    break;
                case 2: 
                    y = Mathf.Lerp(lowY, highY, t);
                    break;
                case 3: 
                    y = Mathf.Lerp(highY, lowY, t);
                    break;
                default: 
                    y = Random.Range(lowY, highY);
                    x += Random.Range(-0.3f, 0.6f) * coinSpacing;
                    break;
            }

            Spawn(coinPrefab, x, y);
        }
    }

    void Spawn(GameObject prefab, float x, float y)
    {
        if (prefab == null) return;
        Instantiate(prefab, new Vector3(x, y, 0f), Quaternion.identity);
    }
}
