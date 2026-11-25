using UnityEngine;

// Put this on every obstacle prefab (the prefab needs a Rigidbody2D set to Kinematic).
// It moves the obstacle to the left so it looks like the player is running towards it,
// and deletes the obstacle when it has gone off the left side of the screen.
public class ObstacleMovement : MonoBehaviour
{
    // 3.6 is about the same speed as the background (loopSpeed 0.1 x BG width 35.8)
    public float speed = 3.6f;

    // when the obstacle goes past this x position it gets destroyed
    public float destroyX = -15f;

    void Start()
    {
        // a Kinematic Rigidbody2D keeps moving with this speed and is not pulled down by gravity.
        // We move it with the Rigidbody2D (and not the Transform) so the player can stand on it.
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
