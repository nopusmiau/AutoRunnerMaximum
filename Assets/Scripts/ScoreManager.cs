using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text coinsText;

    [Header("Scoring")]
    [SerializeField] private float pointsPerSecond = 10f;
    [SerializeField] private float scoreMultiplier = 1f;

    private float score;
    private int coins;
    private bool counting = true;

    public int Score => Mathf.FloorToInt(score);
    public int Coins => coins;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (!counting) return;

        score += pointsPerSecond * scoreMultiplier * Time.deltaTime;
        UpdateText();
    }

    public void AddPoints(int points)
    {
        if (!counting) return;

        score += points * scoreMultiplier;
        UpdateText();
    }

    // Called by Coin when the player picks one up.
    public void AddCoin(int points)
    {
        if (!counting) return;

        coins++;
        AddPoints(points);
    }

    public void SetMultiplier(float multiplier)
    {
        scoreMultiplier = multiplier;
    }

    public void StopCounting()
    {
        counting = false;
    }

    private void UpdateText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Score;

        if (coinsText != null)
            coinsText.text = "Coins: " + coins;
    }
}
