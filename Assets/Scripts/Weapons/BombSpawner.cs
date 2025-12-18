using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour
{
    public int level;
    public GameObject bombPrefab;
    public PlayerMovements player;

    public float distance = 1.5f; // spawn offset in front of player
    private float cooldown;

    void Awake()
    {
        if (player == null)
            player = GetComponentInParent<PlayerMovements>();
    }

    void Start()
    {
        if (level < 1 || bombPrefab == null || player == null)
        {
            gameObject.SetActive(false);
            return;
        }

        UpdateStats();
        StartCoroutine(SpawnLoop());
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: cooldown = 5f; break;
            case 2: cooldown = 4.5f; break;
            case 3: cooldown = 4f; break;
            case 4: cooldown = 3.5f; break;
            case 5: cooldown = 3f; break;
        }
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnBomb();
            yield return new WaitForSeconds(cooldown);
        }
    }

    void SpawnBomb()
    {
        Vector2 dir = SnapDirection(player.lastMoveDir);

        // Spawn bomb slightly in front of player
        Vector2 spawnPos = (Vector2)player.transform.position + dir * distance;
        GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity);

        Bomb b = bomb.GetComponent<Bomb>();
        b.Initialize(dir, level); // bombs know their direction and level
    }

    Vector2 SnapDirection(Vector2 dir)
    {
        if (dir == Vector2.zero) return Vector2.down;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;
        float rad = angle * Mathf.Deg2Rad;

        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
