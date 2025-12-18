using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public int level;
    public float distance = 1.5f;       // distance from player
    public GameObject bulletPrefab;

    public PlayerMovements player;

    private float attackSpeed;
    private float bulletSpeed;
    private float bulletDamage;
    private float bulletDistance;

    private Coroutine shootRoutine;

    void Awake()
    {
        if (player == null)
            player = GetComponentInParent<PlayerMovements>();
    }

    void Start()
    {
        if (level < 1 || bulletPrefab == null || player == null)
        {
            gameObject.SetActive(false);
            return;
        }

        UpdateStats();
        shootRoutine = StartCoroutine(ShootLoop());
    }

    void Update()
    {
        // Make the gun follow the player's last input direction
        Vector2 dir = SnapDirection(player.lastMoveDir);
        Vector2 pos = dir * distance;

        transform.localPosition = pos;

        // Rotate gun to face movement direction (gun sprite faces right by default)
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: bulletDamage = 10; bulletSpeed = 10; bulletDistance = 50; attackSpeed = 1f; break;
            case 2: bulletDamage = 15; bulletSpeed = 12; bulletDistance = 60; attackSpeed = 2f; break;
            case 3: bulletDamage = 20; bulletSpeed = 14; bulletDistance = 70; attackSpeed = 3f; break;
            case 4: bulletDamage = 30; bulletSpeed = 16; bulletDistance = 80; attackSpeed = 4f; break;
            case 5: bulletDamage = 50; bulletSpeed = 20; bulletDistance = 100; attackSpeed = 5f; break;
        }
    }

    IEnumerator ShootLoop()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }

    void Shoot()
    {
        Vector2 dir = SnapDirection(player.lastMoveDir);
        if (dir == Vector2.zero) dir = Vector2.down;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position + (Vector3)(dir * 0.5f), // spawn slightly ahead of gun
            Quaternion.identity
        );

        // Rotate bullet to face direction (bullet sprite faces up by default)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // offset because bullet faces up
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        bullet.GetComponent<Bullet>().Initialize(dir, bulletDamage, bulletSpeed, bulletDistance);
    }

    Vector2 SnapDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
            return Vector2.down;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;
        float rad = Mathf.Deg2Rad * angle;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
