using UnityEngine;

// Put this on every stone prefab (next to its collider, which is NOT a trigger).
// If the player lands on top of the stone, nothing happens and the player can keep running on it.
// If the player runs into the side of the stone (or hits it from below), the game ends.
// Later this can be changed to take away health instead.
public class ObstacleHit : MonoBehaviour
{
    // the player sinks a tiny bit into the stone when landing,
    // so the feet only have to be "almost" on top of the stone
    public float landingTolerance = 0.2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // only react to the player, not to the ground or other stones
        if (collision.gameObject.GetComponent<PlayerController>() == null)
        {
            return;
        }

        float playerFeet = collision.collider.bounds.min.y;
        float stoneTop = GetComponent<Collider2D>().bounds.max.y;

        if (playerFeet >= stoneTop - landingTolerance)
        {
            // the player landed on top of the stone, so the game goes on
            return;
        }

        Debug.Log("Player hit " + gameObject.name + " - Game Over");
        EndGame();
    }

    private void EndGame()
    {
#if UNITY_EDITOR
        // Application.Quit() does nothing inside the editor, so stop Play mode instead
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
