using UnityEngine;

public class DeadlyObstacle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player touched " + transform.root.name + " - Game Over");
            GameOver.EndGame();
        }
    }
}
