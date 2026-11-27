using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 1;
    public bool isPlayer = false;

    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !isPlayer)
        {
            Kill();
            return true;
        }
        else if (currentHealth <= 0 && isPlayer)
        {
            // Handle player death logic here
            Debug.Log("Player has died!");
            EndGame();
            return true;
        }

        return false;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void EndGame()
    {
    #if UNITY_EDITOR
            // Application.Quit() does nothing inside the editor, so stop Play mode instead
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
    }
}