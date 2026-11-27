using UnityEngine;

// Turns the obstacle around all the time (used for the saw blade).
public class Spin : MonoBehaviour
{
    // how fast it turns (positive = anticlockwise, like a wheel rolling to the left)
    public float degreesPerSecond = 360f;

    void Update()
    {
        transform.Rotate(0, 0, degreesPerSecond * Time.deltaTime);
    }
}
