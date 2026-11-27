using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 1;
    public bool isPlayer = false;

    public TMP_Text healthText;
    private int currentHealth;
    public GameObject gameOverPanel;
    public AudioSource bgMusic;
    public AudioManager audioManager;
    public TMP_Text highScore;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update(){
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthText.text = "Health: " + currentHealth;

        if (currentHealth <= 0 && !isPlayer)
        {
            Kill();
            return true;
        }
        else if (currentHealth <= 0 && isPlayer)
        {
            // Handle player death logic here
            audioManager.PlaySound(1);
            Debug.Log("Player has died!");
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            bgMusic.Pause();
            if (ScoreManager.Instance != null)
            {
                int score = (int) ScoreManager.Instance.score;
                highScore.text = "High Score: " + score;
            }
            return true;
        }

        return false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}