using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f;
    private float nextDamageTime;
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player == null)
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
