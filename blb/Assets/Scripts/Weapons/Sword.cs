using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour
{
    public float range = 1f;
    public float attackSpeed = 1f;
    public float damage = 10;
    public int level;
    public float distance = 1.5f;
    public float verticalOffset = 0.2f;

    public PlayerMovements player;
    public SpriteRenderer sprite;

    void Awake()
    {
        player = GetComponentInParent<PlayerMovements>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (level < 1)
        {
            gameObject.SetActive(false);
            return;
        }
        StartCoroutine(Attack());
    }

    void Update()
    {
        UpdateStats();
        Vector2 dir = SnapDirection(player.lastMoveDir);

        // Base position in front of player
        Vector2 pos = dir * distance;

        // Visual depth (above/below player)
        if (dir.y > 0)        // looking up
            pos.y += verticalOffset;
        else if (dir.y < 0)   // looking down
            pos.y -= verticalOffset;

        transform.localPosition = pos;

        // Rotate sword to face the movement direction
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
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

    IEnumerator Attack()
    {
        while (true)
        {
            // ATTACK ON
            sprite.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            // ATTACK OFF
            sprite.color = Color.white;

            float cooldown = 1f / attackSpeed;
            yield return new WaitForSeconds(cooldown);
        }
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: damage = 10; attackSpeed = 1f; range = 1f; break;
            case 2: damage = 15; attackSpeed = 1.2f; range = 1.2f; break;
            case 3: damage = 20; attackSpeed = 1.5f; range = 1.5f; break;
            case 4: damage = 30; attackSpeed = 1.8f; range = 1.8f; break;
            case 5: damage = 50; attackSpeed = 2f; range = 2f; break;
        }
    }
}
