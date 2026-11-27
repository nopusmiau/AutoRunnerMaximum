using System.Collections;
using UnityEngine;

// Put this on the Player (next to PlayerMovement).
// It keeps track of the timed powerups so they still end after the pickup has been destroyed.
public class PlayerPowerups : MonoBehaviour
{
    // ObstacleHit checks this before ending the game
    public static bool Invincible { get; private set; }

    [Header("Colours while a powerup is active")]
    public Color shieldColor = new Color(1f, 0.85f, 0.3f);  // gold
    public Color speedColor = new Color(0.5f, 0.85f, 1f);   // light blue

    private PlayerMovement movement;
    private float normalJumpForce;
    private SpriteRenderer[] sprites;
    private Color[] normalColors;

    private Coroutine invincibleRoutine;
    private Coroutine jumpRoutine;
    private Coroutine speedRoutine;
    private bool speeding;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        if (movement != null) normalJumpForce = movement.jumpForce;

        sprites = GetComponentsInChildren<SpriteRenderer>();
        normalColors = new Color[sprites.Length];
        for (int i = 0; i < sprites.Length; i++) normalColors[i] = sprites[i].color;

        Invincible = false;
    }

    void OnDestroy()
    {
        Invincible = false;
        Time.timeScale = 1f;
    }

    public void ActivateInvincibility(float seconds)
    {
        if (invincibleRoutine != null) StopCoroutine(invincibleRoutine);
        invincibleRoutine = StartCoroutine(InvincibleRoutine(seconds));
    }

    public void ActivateHighJump(float multiplier, float seconds)
    {
        if (movement == null) return;
        if (jumpRoutine != null) StopCoroutine(jumpRoutine);
        jumpRoutine = StartCoroutine(HighJumpRoutine(multiplier, seconds));
    }

    public void ActivateSpeedBoost(float multiplier, float seconds)
    {
        if (speedRoutine != null) StopCoroutine(speedRoutine);
        speedRoutine = StartCoroutine(SpeedRoutine(multiplier, seconds));
    }

    private IEnumerator InvincibleRoutine(float seconds)
    {
        Invincible = true;
        UpdateColour();
        // real seconds, so a speed boost doesn't make the shield end early
        yield return new WaitForSecondsRealtime(seconds);
        Invincible = false;
        UpdateColour();
    }

    private IEnumerator HighJumpRoutine(float multiplier, float seconds)
    {
        movement.jumpForce = normalJumpForce * multiplier;
        yield return new WaitForSecondsRealtime(seconds);
        movement.jumpForce = normalJumpForce;
    }

    private IEnumerator SpeedRoutine(float multiplier, float seconds)
    {
        // speeds up the whole game: obstacles, coins, background and the score timer
        speeding = true;
        Time.timeScale = multiplier;
        UpdateColour();
        yield return new WaitForSecondsRealtime(seconds);
        Time.timeScale = 1f;
        speeding = false;
        UpdateColour();
    }

    private void UpdateColour()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i] == null) continue;

            if (Invincible) sprites[i].color = shieldColor;
            else if (speeding) sprites[i].color = speedColor;
            else sprites[i].color = normalColors[i];
        }
    }
}
