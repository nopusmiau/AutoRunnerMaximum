
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParryZone : MonoBehaviour
{
    [Header("Parry Settings")]
    public float parryDuration = 0.25f;
    public float parryCooldown = 0.75f;

    private bool isParrying;
    private bool canParry = true;

    private float parryTimer;
    private float cooldownTimer;
    public Animator animator;
    public AudioManager audioManager;

    void Update()
    {
        // Detect E key
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryParry();
        }

        // Parry duration
        if (isParrying)
        {
            parryTimer -= Time.deltaTime;

            if (parryTimer <= 0f)
                isParrying = false;
        }

        // Cooldown
        if (!canParry)
        {
            cooldownTimer -= Time.deltaTime;

            if (cooldownTimer <= 0f)
                canParry = true;
        }
    }

    public void TryParry()
    {
        if (!canParry)
            return;

        isParrying = true;
        canParry = false;

        parryTimer = parryDuration;
        cooldownTimer = parryCooldown;
        animator.SetTrigger("Deflect");
        audioManager.PlaySound(2);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isParrying)
            return;

        Projectile projectile = other.GetComponent<Projectile>();

        if (projectile != null && projectile.Parry())
        {
            Debug.Log("SUCCESSFUL PARRY!");
        }
    }
}
