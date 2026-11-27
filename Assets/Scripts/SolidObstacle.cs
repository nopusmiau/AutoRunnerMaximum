using UnityEngine;

public class SolidObstacle : MonoBehaviour
{
    public float landingTolerance = 0.2f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() == null)
        {
            return;
        }

        float playerFeet = collision.collider.bounds.min.y;
        float obstacleTop = collision.otherCollider.bounds.max.y;

        if (playerFeet >= obstacleTop - landingTolerance)
        {
            return;
        }

        Debug.Log("Player ran into " + gameObject.name + " - Game Over");
        GameOver.EndGame();
    }
}
