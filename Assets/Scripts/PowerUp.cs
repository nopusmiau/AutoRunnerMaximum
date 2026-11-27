using UnityEngine;

public enum PowerUpType
{
    Invincibility,
    HighJump,
    SpeedBoost
}

// Put this on a powerup prefab (needs a Collider2D with "Is Trigger" ticked).
// Pick the type in the Inspector. It scrolls left like the obstacles.
[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp : MonoBehaviour
{
    public PowerUpType type = PowerUpType.Invincibility;
    public float duration = 5f;          // seconds the effect lasts
    public float jumpMultiplier = 1.3f;  // only used by HighJump
    public float speedMultiplier = 1.5f; // only used by SpeedBoost
    public int bonusPoints = 0;          // optional points for picking it up

    public float speed = 3.6f;           // same as ObstacleMovement
    public float destroyX = -15f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector2.left * speed;
    }

    void Update()
    {
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        PlayerPowerups powerups = player.GetComponent<PlayerPowerups>();
        if (powerups == null)
        {
            powerups = player.gameObject.AddComponent<PlayerPowerups>();
        }

        switch (type)
        {
            case PowerUpType.Invincibility:
                powerups.ActivateInvincibility(duration);
                break;
            case PowerUpType.HighJump:
                powerups.ActivateHighJump(jumpMultiplier, duration);
                break;
            case PowerUpType.SpeedBoost:
                powerups.ActivateSpeedBoost(speedMultiplier, duration);
                break;
        }

        if (bonusPoints > 0 && ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(bonusPoints);
        }
        Destroy(gameObject);
    }
}
