using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 3.6f;
    public float destroyX = -15f;

    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * speed;
    }

    void Update()
    {
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
