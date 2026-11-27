using UnityEngine;

public class SpriteBlink : MonoBehaviour
{
    public Sprite[] frames;
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

            currentFrame = currentFrame + 1;
            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
