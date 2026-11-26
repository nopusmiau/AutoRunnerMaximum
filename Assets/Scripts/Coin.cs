using UnityEngine;

// Put this on the coin prefab (needs a Collider2D with "Is Trigger" ticked).
// It scrolls left at the same speed as the obstacles and gives points when the player touches it.
[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    public int value = 10;
    public float speed = 3.6f;     // same as ObstacleMovement
    public float destroyX = -15f;  // same as ObstacleMovement

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
        // same way ObstacleHit recognises the player
        if (other.GetComponentInParent<PlayerController>() == null) return;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(value);
        }
        Destroy(gameObject);
    }
}
