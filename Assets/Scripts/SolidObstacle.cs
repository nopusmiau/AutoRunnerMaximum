using UnityEngine;

// Put this on obstacles the player can stand on, like walls, blocks and bridges
// (their collider is NOT a trigger, and the prefab has the tag "Platform").
// If the player lands on top, nothing happens and the player can keep running on it.
// If the player runs into the side (or hits it from below), the game ends.
public class SolidObstacle : MonoBehaviour
{
    // the player sinks a tiny bit into the obstacle when landing,
    // so the feet only have to be "almost" on top
    public float landingTolerance = 0.2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // only react to the player
        if (collision.gameObject.GetComponent<PlayerController>() == null)
        {
            return;
        }

        // collision.collider is the player's collider,
        // collision.otherCollider is the collider of this obstacle that was hit
        float playerFeet = collision.collider.bounds.min.y;
        float obstacleTop = collision.otherCollider.bounds.max.y;

        if (playerFeet >= obstacleTop - landingTolerance)
        {
            // the player landed on top, so the game goes on
            return;
        }

        Debug.Log("Player ran into " + gameObject.name + " - Game Over");
        GameOver.EndGame();
    }
}
