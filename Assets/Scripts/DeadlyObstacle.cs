using UnityEngine;

// Put this on an obstacle (or a part of an obstacle) that has a TRIGGER collider.
// Touching it in any way ends the game: spikes, lava, saw blades, mines, puddles, ...
// The player has to jump over it (or slide under it if it is floating).
public class DeadlyObstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // only react to the player
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player touched " + transform.root.name + " - Game Over");
            GameOver.EndGame();
        }
    }
}
