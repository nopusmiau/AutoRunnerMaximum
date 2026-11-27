using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 7.5f;
    public float jumpBufferTime = 0.2f;
    public float slideHeight = 0.5f;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator animator;

    private bool isGrounded = false;
    private bool isSliding = false;
    private float jumpBufferTimer = 0f;

    private Vector2 normalSize;
    private Vector2 normalOffset;

    public AudioManager audioManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        normalSize = box.size;
        normalOffset = box.offset;
        animator.SetBool("isRunning",true);
    }

    void Update()
    {
        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame
                        || Keyboard.current.wKey.wasPressedThisFrame
                        || Keyboard.current.upArrowKey.wasPressedThisFrame;

        bool slideHeld = Keyboard.current.sKey.isPressed
                      || Keyboard.current.downArrowKey.isPressed;

        if (jumpPressed)
        {
            jumpBufferTimer = jumpBufferTime;
        }

        if (jumpBufferTimer > 0 && isGrounded && !isSliding)
        {
            Jump();
        }

        jumpBufferTimer -= Time.deltaTime;

        if (slideHeld && isGrounded && !isSliding)
        {
            StartSlide();
        }

        if (!slideHeld && isSliding)
        {
            StopSlide();
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        jumpBufferTimer = 0;
        animator.SetBool("isJumping", true);
        audioManager.PlaySound(0);
    }

    private void StartSlide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        audioManager.PlaySound(3);

        float newHeight = normalSize.y * slideHeight;
        box.size = new Vector2(normalSize.x, newHeight);

        float moveDown = (normalSize.y - newHeight) / 2;
        box.offset = new Vector2(normalOffset.x, normalOffset.y - moveDown);
    }

    private void StopSlide()
    {
        isSliding = false;
        animator.SetBool("isSliding", false);

        box.size = normalSize;
        box.offset = normalOffset;
    }

    private bool IsGroundOrPlatform(GameObject other)
    {
        return other.CompareTag("Ground") || other.CompareTag("Platform");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        CheckGroundCollision(collision);
    }

    private void CheckGroundCollision(Collision2D collision)
    {
        if (!IsGroundOrPlatform(collision.gameObject))
            return;

        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Only count contact with an upward-facing surface
            if (contact.normal.y > 0.5f &&
                rb.linearVelocity.y <= 0.1f)
            {
                isGrounded = true;

                animator.SetBool("isJumping", false);
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGroundOrPlatform(collision.gameObject))
        {
            isGrounded = false;
        }
    }
}
