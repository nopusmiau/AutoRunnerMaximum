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

    // counts down after jump is pressed, the jump can happen while it is above 0
    private float jumpBufferTimer = 0f;

    // the normal collider size, so we can put it back after sliding
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

    private void Jump()
    {
        // give the player an upward speed, gravity will pull it back down
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsGroundOrStone(collision.gameObject))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (IsGroundOrStone(collision.gameObject))
        {
            isGrounded = false;
        }
    }
}
