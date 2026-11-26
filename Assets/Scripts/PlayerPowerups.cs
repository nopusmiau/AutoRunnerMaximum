using System.Collections;
using UnityEngine;

// Put this on the Player (next to PlayerMovement).
// It keeps track of the timed powerups so they still end after the pickup has been destroyed.
public class PlayerPowerups : MonoBehaviour
{
    // ObstacleHit checks this before ending the game
    public static bool Invincible { get; private set; }

    private PlayerMovement movement;
    private float normalJumpForce;
    private Coroutine invincibleRoutine;
    private Coroutine jumpRoutine;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        normalJumpForce = movement.jumpForce;
        Invincible = false;
    }

    void OnDestroy()
    {
        Invincible = false;
    }

    public void ActivateInvincibility(float seconds)
    {
        if (invincibleRoutine != null) StopCoroutine(invincibleRoutine);
        invincibleRoutine = StartCoroutine(InvincibleRoutine(seconds));
    }

    public void ActivateHighJump(float multiplier, float seconds)
    {
        if (jumpRoutine != null) StopCoroutine(jumpRoutine);
        jumpRoutine = StartCoroutine(HighJumpRoutine(multiplier, seconds));
    }

    private IEnumerator InvincibleRoutine(float seconds)
    {
        Invincible = true;
        yield return new WaitForSeconds(seconds);
        Invincible = false;
    }

    private IEnumerator HighJumpRoutine(float multiplier, float seconds)
    {
        movement.jumpForce = normalJumpForce * multiplier;
        yield return new WaitForSeconds(seconds);
        movement.jumpForce = normalJumpForce;
    }
}
