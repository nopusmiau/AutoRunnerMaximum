using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Scoring")]
    [SerializeField] private float pointsPerSecond = 10f; 
    [SerializeField] private float scoreMultiplier = 1f;  

    public float score;
    private bool counting = true;

    public int Score => Mathf.FloorToInt(score);

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (!counting) return;

        score += pointsPerSecond * scoreMultiplier * Time.deltaTime;
        UpdateText();
    }

    public void AddPoints(int points)
    {
        score += points * scoreMultiplier;
        UpdateText();
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
    }
}
