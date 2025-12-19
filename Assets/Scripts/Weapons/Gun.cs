using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
    public int level;
    public float distance = 1.5f;       
    public GameObject bulletPrefab;

    public PlayerMovements player;

    private float attackSpeed;
    private float bulletSpeed;
    private float bulletDamage;
    private float bulletDistance;
    private int bulletsPerShot;          

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
        Vector2 dir = SnapDirection(player.lastMoveDir);
        Vector2 pos = dir * distance;
        transform.localPosition = pos;

        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (player.transform.localScale.x < 0)
                angle += 180f;

            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: bulletDamage = 10; bulletSpeed = 10; bulletDistance = 50; attackSpeed = 1f; bulletsPerShot = 1; break;
            case 2: bulletDamage = 15; bulletSpeed = 12; bulletDistance = 60; attackSpeed = 2f; bulletsPerShot = 3; break;
            case 3: bulletDamage = 20; bulletSpeed = 14; bulletDistance = 70; attackSpeed = 3f; bulletsPerShot = 3; break;
            case 4: bulletDamage = 30; bulletSpeed = 16; bulletDistance = 80; attackSpeed = 4f; bulletsPerShot = 5; break;
            case 5: bulletDamage = 50; bulletSpeed = 20; bulletDistance = 100; attackSpeed = 5f; bulletsPerShot = 5; break;
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

        float[] angles;

        if (bulletsPerShot == 1)
            angles = new float[] { 0f };
        else if (bulletsPerShot == 3)
            angles = new float[] { -15f, 0f, 15f };
        else // bulletsPerShot == 5
            angles = new float[] { -30f, -15f, 0f, 15f, 30f };
        // On a maintenant la balle centrale toujours incluse

        foreach (float a in angles)
        {
            Vector2 shotDir = RotateVector(dir, a);

            GameObject bullet = Instantiate(
                bulletPrefab,
                transform.position + (Vector3)(shotDir * 0.5f),
                Quaternion.identity
            );

            float angle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg - 90f;

            if (player.transform.localScale.x < 0)
                angle += 180f;

            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            bullet.GetComponent<Bullet>().Initialize(shotDir, bulletDamage, bulletSpeed, bulletDistance);
        }
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

    Vector2 RotateVector(Vector2 v, float degrees)
    {
        float rad = degrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);
        return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos).normalized;
    }
}
