using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Coin : MonoBehaviour
{
    public int value = 10;
    public float speed = 3.6f;     
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
        
        if (other.GetComponentInParent<PlayerController>() == null) return;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddCoin(value);
        }
        Destroy(gameObject);
    }
}
