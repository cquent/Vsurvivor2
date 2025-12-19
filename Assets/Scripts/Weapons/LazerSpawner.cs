using UnityEngine;
using System.Collections;

public class LazerSpawner : MonoBehaviour
{
    public int level;
    public GameObject lazerPrefab;
    public float distance = 2.8f; // offset from player

    public PlayerMovements player;
    private float attackSpeed;

    void Awake()
    {
        if (player == null)
            player = GetComponentInParent<PlayerMovements>();
    }

    void Start()
    {
        if (level < 1 || lazerPrefab == null || player == null)
        {
            gameObject.SetActive(false);
            return;
        }

        UpdateStats();
        StartCoroutine(FireLoop());
    }

    void Update()
    {
        // Make spawner follow player direction
        Vector2 dir = SnapDirection(player.lastMoveDir);
        transform.localPosition = dir * distance;

        // Rotate spawner itself (sprite faces right by default)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // Compensate if player is flipped left
        if (player.transform.localScale.x < 0)
            angle += 180f;

        transform.localEulerAngles = new Vector3(0, 0, angle);
    }


    void UpdateStats()
    {
        attackSpeed = 0.8f + 0.3f * level;
    }

    IEnumerator FireLoop()
    {
        while (true)
        {
            SpawnLazer();
            yield return new WaitForSeconds(1f / attackSpeed);
        }
    }

    void SpawnLazer()
    {
        Vector2 dir = SnapDirection(player.lastMoveDir);
        Vector2 spawnPos = (Vector2)transform.position;

        GameObject lazer = Instantiate(lazerPrefab, spawnPos, Quaternion.identity);
        lazer.GetComponent<Lazer>().Initialize(level, player, dir);
    }

    Vector2 SnapDirection(Vector2 dir)
    {
        if (dir == Vector2.zero) return Vector2.down;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;
        float rad = Mathf.Deg2Rad * angle;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}