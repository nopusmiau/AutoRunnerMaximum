using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Put this on the Player (the Player needs a Rigidbody2D, a BoxCollider2D and an Animator).
// The ground object must have the tag "Ground" and the stone prefabs the tag "Stone".
//
// Jump:  press Space, W or Up Arrow   -> to jump over the rocks
// Slide: hold S or Down Arrow         -> to go under the floating ledge
//
// The animations are switched with the Animator parameters "isJumping" and "isSliding".
public class PlayerMovement : MonoBehaviour
{
    // how strong the jump is (bigger number = higher and longer jump)
    public float jumpForce = 7.5f;

    // if jump is pressed a little before landing, the game remembers it for this long (in seconds)
    // and jumps as soon as the player touches the ground
    public float jumpBufferTime = 0.2f;

    // how tall the collider is while sliding (0.5 = half as tall)
    public float slideHeight = 0.5f;

    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Animator animator;

    private bool isGrounded = false;
    private bool isSliding = false;

    // everything the player is currently standing on (ground or the top of a stone).
    // Keeping a list instead of one true/false means leaving one thing
    // (e.g. a stone that gets smashed or scrolls away) doesn't cancel standing on another.
    private readonly HashSet<Collider2D> standingOn = new HashSet<Collider2D>();

    // counts down after jump is pressed, the jump can happen while it is above 0
    private float jumpBufferTimer = 0f;

    // right after jumping, ignore the ground for a moment so the player
    // isn't counted as "still standing" on the frame the jump starts
    private float ignoreGroundTimer = 0f;
    private const float IgnoreGroundAfterJump = 0.1f;

    // the normal collider size, so we can put it back after sliding
    private Vector2 normalSize;
    private Vector2 normalOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        normalSize = box.size;
        normalOffset = box.offset;
    }

    void Update()
    {
        UpdateGrounded();

        bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame
                        || Keyboard.current.wKey.wasPressedThisFrame
                        || Keyboard.current.upArrowKey.wasPressedThisFrame;

        bool slideHeld = Keyboard.current.sKey.isPressed
                      || Keyboard.current.downArrowKey.isPressed;

        // remember the jump press for a short time
        if (jumpPressed)
        {
            jumpBufferTimer = jumpBufferTime;
        }

        // you can only jump when standing on the ground
        // (because of the timer, pressing jump just before landing also works)
        if (jumpBufferTimer > 0 && isGrounded && !isSliding)
        {
            Jump();
        }

        jumpBufferTimer -= Time.deltaTime;
        ignoreGroundTimer -= Time.deltaTime;

        // you can only start sliding when on the ground
        if (slideHeld && isGrounded && !isSliding)
        {
            StartSlide();
        }

        // stand up again when the slide key is let go
        if (!slideHeld && isSliding)
        {
            StopSlide();
        }
    }

    private void UpdateGrounded()
    {
        // forget stones that were destroyed (smashed by the shield or scrolled off screen)
        standingOn.RemoveWhere(c => c == null || !c.enabled || !c.gameObject.activeInHierarchy);

        bool wasGrounded = isGrounded;
        isGrounded = ignoreGroundTimer <= 0f && standingOn.Count > 0;

        // just landed: switch back from the jump animation
        if (isGrounded && !wasGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }

    private void Jump()
    {
        // give the player an upward speed, gravity will pull it back down
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
        standingOn.Clear();
        ignoreGroundTimer = IgnoreGroundAfterJump;
        jumpBufferTimer = 0;
        animator.SetBool("isJumping", true);
    }

    private void StartSlide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);

        // make the collider shorter so the player fits under the ledge
        float newHeight = normalSize.y * slideHeight;
        box.size = new Vector2(normalSize.x, newHeight);

        // move the collider down so its bottom stays at the feet
        float moveDown = (normalSize.y - newHeight) / 2;
        box.offset = new Vector2(normalOffset.x, normalOffset.y - moveDown);
    }

    private void StopSlide()
    {
        isSliding = false;
        animator.SetBool("isSliding", false);

        // back to the normal collider
        box.size = normalSize;
        box.offset = normalOffset;
    }

    // the player can stand on the ground and also on top of a stone
    // (if the player runs into the side of a stone, ObstacleHit ends the game)
    private bool IsGroundOrStone(GameObject other)
    {
        return other.CompareTag("Ground") || other.CompareTag("Stone");
    }

    // true if this contact is underneath the player (not the side of a stone)
    private bool IsUnderFeet(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f) return true;
        }
        return false;
    }

    private void CheckContact(Collision2D collision)
    {
        if (!IsGroundOrStone(collision.gameObject)) return;

        if (IsUnderFeet(collision))
            standingOn.Add(collision.collider);
        else
            standingOn.Remove(collision.collider);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        CheckContact(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        CheckContact(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        standingOn.Remove(collision.collider);
    }
}
