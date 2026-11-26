
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float minPositionX = 4.5f;
    public float maxPositionX = 5.5f;

    [Header("Wobble")]
    public float wobbleAmount = 0.4f;
    public float wobbleSpeed = 2f;

    [Header("Stay Duration")]
    public float stayDuration = 20f;

    [Header("Target")]
    public Transform player;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;

    [Header("Cleanup")]
    public float destroyX = -15f;

    private float nextFireTime;
    private float stayTimer;

    private float baseY;
    private float wobbleTimer;
    private float stayPositionX;
    private float targetStayX;

    private enum EnemyState
    {
        Entering,
        Staying,
        Leaving
    }

    private EnemyState currentState = EnemyState.Entering;

    void Start()
    {
        baseY = transform.position.y;

        // Choose a random staying position
        targetStayX = Random.Range(
            minPositionX,
            maxPositionX
        );
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Entering:
                EnterScreen();
                break;

            case EnemyState.Staying:
                StayOnScreen();
                break;

            case EnemyState.Leaving:
                LeaveScreen();
                break;
        }

        // Continue shooting while on screen
        if (currentState != EnemyState.Leaving)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }

        // Destroy after leaving
        if (transform.position.x <= destroyX)
        {
            Destroy(gameObject);
        }
    }

    void EnterScreen()
    {
        // Move smoothly toward the chosen staying position
        Vector3 position = transform.position;

        position.x = Mathf.MoveTowards(
            position.x,
            targetStayX,
            moveSpeed * Time.deltaTime
        );

        transform.position = position;

        // Start staying once the target is reached
        if (Mathf.Abs(transform.position.x - targetStayX) < 0.001f)
        {
            stayPositionX = targetStayX;

            currentState = EnemyState.Staying;

            baseY = transform.position.y;
            stayTimer = stayDuration;
        }
    }

    void StayOnScreen()
    {
        // Slight vertical wobble
        wobbleTimer += Time.deltaTime;

        float wobbleY =
            Mathf.Sin(wobbleTimer * wobbleSpeed) * wobbleAmount;

        transform.position = new Vector3(
            stayPositionX,
            baseY + wobbleY,
            transform.position.z
        );

        // Count down stay duration
        stayTimer -= Time.deltaTime;

        if (stayTimer <= 0f)
        {
            currentState = EnemyState.Leaving;
        }
    }

    void LeaveScreen()
    {
        transform.Translate(
            Vector2.left * moveSpeed * Time.deltaTime,
            Space.World
        );
    }

    void Shoot()
    {
        if (player == null || projectilePrefab == null || firePoint == null)
            return;

        Vector2 direction =
            ((Vector2)player.position -
            (Vector2)firePoint.position).normalized;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.identity
        );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.SetDirection(direction);
            projectileScript.SetShooter(transform);
        }
    }
}
