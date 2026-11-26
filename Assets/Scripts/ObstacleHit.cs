using UnityEngine;

// Put this on every stone prefab (next to its collider, which is NOT a trigger).
// If the player lands on top of the stone, nothing happens and the player can keep running on it.
// If the player runs into the side of the stone (or hits it from below), the game ends.
// Later this can be changed to take away health instead.
public class ObstacleHit : MonoBehaviour
{
    // the player sinks a tiny bit into the stone when landing,
    // so the feet only have to be "almost" on top of the stone
    public float landingTolerance = 0.2f;

    [Header("Damage")]
    public int damage = 1;
    public float damageCooldown = 1f;

    private float nextDamageTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // only react to the player, not to the ground or other stones
        PlayerController player =
            collision.gameObject.GetComponentInParent<PlayerController>();

        if (player == null)
            return;

        float playerFeet = collision.collider.bounds.min.y;
        float stoneTop = GetComponent<Collider2D>().bounds.max.y;

        if (playerFeet >= stoneTop - landingTolerance)
        {
            // the player landed on top of the stone, so the game goes on
            return;
        }
        if (Time.time >= nextDamageTime)
            {
                EnemyHealth playerHealth =
                    player.GetComponent<EnemyHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);

                    nextDamageTime = Time.time + damageCooldown;

                    Debug.Log("Player hit obstacle: " + gameObject.name);
                }
            }
        //Debug.Log("Player hit " + gameObject.name + " - Game Over");
       // EndGame();
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
