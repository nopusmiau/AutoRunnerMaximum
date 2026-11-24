using UnityEngine;

// Put this on every obstacle prefab (next to its trigger collider).
// For now the game just stops when the player touches an obstacle.
// Later this can be changed to take away health instead.
public class ObstacleHit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // only react to the player, not to other obstacles or the ground
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player hit " + gameObject.name + " - Game Over");
            EndGame();
        }
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
