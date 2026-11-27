using UnityEngine;

public class SolidObstacle : MonoBehaviour
{
    public float landingTolerance = 0.2f;
    public int damage = 1;
    public float damageCooldown = 1f;
    private float nextDamageTime;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null)
        {
            return;
        }

        float playerFeet = collision.collider.bounds.min.y;
        float obstacleTop = collision.otherCollider.bounds.max.y;

        if (playerFeet >= obstacleTop - landingTolerance)
        {
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


    }
}
