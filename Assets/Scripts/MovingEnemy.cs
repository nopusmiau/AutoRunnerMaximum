
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [Header("Entering")]
    public float moveSpeed = 5f;
    public float minPositionX = 4.5f;
    public float maxPositionX = 5.5f;

    [Header("Erratic Movement")]
    public float minWaitTime = 2f;
    public float maxWaitTime = 5f;

    public float horizontalSpeed = 2f;
    public float heightChange = 0.15f;
    public float movementChangeSpeed = 3f;

    [Header("Charge")]
    public float chargeSpeed = 10f;

    [Header("Ground")]
    public float groundY = 0f;

    [Header("Collision Damage")]
    public int collisionDamage = 1;
    public float damageCooldown = 1f;

    [Header("Cleanup")]
    public float destroyX = -15f;

    private float waitTimer;
    private float nextChargeTime;

    private float movementTimer;
    private float baseY;
    private float targetY;
    private float targetX;

    private float nextDamageTime;

    private enum EnemyState
    {
        Entering,
        Wandering,
        Charging
    }

    private EnemyState currentState = EnemyState.Entering;

    void Start()
    {
        baseY = groundY;
        targetY = baseY;
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Entering:
                EnterScreen();
                break;

            case EnemyState.Wandering:
                Wander();
                break;

            case EnemyState.Charging:
                Charge();
                break;
        }

        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }

    void EnterScreen()
    {
        transform.Translate(
            Vector2.left * moveSpeed * Time.deltaTime,
            Space.World
        );

        if (transform.position.x <= maxPositionX)
        {
            transform.position = new Vector3(
                maxPositionX,
                groundY,
                transform.position.z
            );

            baseY = groundY;
            waitTimer = 0f;

            nextChargeTime = Random.Range(
                minWaitTime,
                maxWaitTime
            );

            currentState = EnemyState.Wandering;

            SetNextMovementTarget();
        }
    }

    void Wander()
    {
        Vector3 position = transform.position;

        position.x = Mathf.MoveTowards(
            position.x,
            targetX,
            horizontalSpeed * Time.deltaTime
        );

        position.y = Mathf.Lerp(
            position.y,
            targetY,
            Time.deltaTime * 3f
        );

        transform.position = position;

        movementTimer -= Time.deltaTime;

        if (movementTimer <= 0f)
        {
            SetNextMovementTarget();
        }

        waitTimer += Time.deltaTime;

        if (waitTimer >= nextChargeTime)
        {
            currentState = EnemyState.Charging;
        }
    }

    void SetNextMovementTarget()
    {
        targetX = Random.Range(
            minPositionX,
            maxPositionX
        );

        targetY = baseY + Random.Range(
            -heightChange,
            heightChange
        );

        movementTimer = Random.Range(
            0.2f,
            movementChangeSpeed
        );
    }

    void Charge()
    {
        transform.Translate(
            Vector2.left * chargeSpeed * Time.deltaTime,
            Space.World
        );
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time < nextDamageTime)
            return;

        EnemyHealth playerHealth = collision.gameObject.GetComponentInParent<EnemyHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(collisionDamage);

            nextDamageTime = Time.time + damageCooldown;
        }
    }
}
