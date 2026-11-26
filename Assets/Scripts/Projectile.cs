
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 8f;
    public float reflectedSpeedMultiplier = 2f;

    [Header("Lifetime")]
    public float lifetime = 5f;

    [Header("Cleanup")]
    public float destroyBelowY = -10f;

    private Vector2 direction;
    private float currentSpeed;

    private bool isReflected;
    private Transform originalShooter;

    void Start()
    {
        currentSpeed = speed;

        // Automatically destroy after lifetime
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetShooter(Transform shooter)
    {
        originalShooter = shooter;
    }

    
    public bool Parry()
    {
        // No original shooter left
        if (originalShooter == null)
        {
            Destroy(gameObject);
            return false;
        }

        if (isReflected)
            return false;

        isReflected = true;

        // Double projectile speed
        currentSpeed = speed * reflectedSpeedMultiplier;

        return true;
    }


    void Update()
    {
        // Home in on the original shooting enemy
        if (isReflected && originalShooter != null)
        {
            direction = (
                (Vector2)originalShooter.position -
                (Vector2)transform.position
            ).normalized;
        }

        transform.Translate(
            direction * currentSpeed * Time.deltaTime,
            Space.World
        );

        if (transform.position.y < destroyBelowY)
        {
            Destroy(gameObject);
            return;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isReflected)
            return;

        ShootingEnemy enemy = other.GetComponentInParent<ShootingEnemy>();

        if (enemy != null && enemy.transform == originalShooter)
        {
            EnemyHealth health =
                enemy.GetComponent<EnemyHealth>();

            if (health != null)
            {
                health.TakeDamage(1);
            }
            else
            {
                Destroy(enemy.gameObject);
            }

            Destroy(gameObject);
        }
    }
}
