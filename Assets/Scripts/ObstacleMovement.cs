using UnityEngine;

// Put this on every obstacle prefab.
// It moves the obstacle to the left so it looks like the player is running towards it,
// and deletes the obstacle when it has gone off the left side of the screen.
public class ObstacleMovement : MonoBehaviour
{
    // 3.6 is about the same speed as the background (loopSpeed 0.1 x BG width 35.8)
    public float speed = 3.6f;

    // when the obstacle goes past this x position it gets destroyed
    public float destroyX = -15f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
