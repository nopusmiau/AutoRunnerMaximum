using UnityEngine;

// Switches between a few pictures over and over, so the obstacle looks like it is blinking.
// Used for the land mine light, the electric orb and the magnet.
public class SpriteBlink : MonoBehaviour
{
    // drag the pictures in here from the Inspector
    public Sprite[] frames;

    // how long each picture is shown (in seconds)
    public float timePerFrame = 0.3f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timePerFrame)
        {
            timer = 0f;

            // go to the next picture, and back to the first one after the last one
            currentFrame = currentFrame + 1;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
