using UnityEngine;
using System.Collections;

public class Boomerang : MonoBehaviour
{
    public int level;

    public float speed = 5f;
    public float maxDistance = 3f;
    public float damage = 10f;
    public float spinSpeed = 360f; // degrees per second

    public PlayerMovements player;
    public Vector2 startDir;
    public Vector2 startPos;

    void Awake()
    {
        player = GetComponentInParent<PlayerMovements>();
    }

    void Start()
    {
        if (level < 1)
        {
            gameObject.SetActive(false);
            return;
        }
        UpdateStats();
        startDir = SnapDirection(player.lastMoveDir);
        startPos = transform.position;

        StartCoroutine(Throw());
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1:
                damage = 10;
                maxDistance = 5f;
                transform.localScale = Vector3.one * 0.8f;
                break;
            case 2:
                damage = 15;
                maxDistance = 7f;
                transform.localScale = Vector3.one;
                speed = 7f;
                break;
            case 3:
                damage = 25;
                maxDistance = 9f;
                transform.localScale = Vector3.one * 1.3f;
                speed = 9f;
                break;
            case 4:
                damage = 40;
                maxDistance = 12f;
                transform.localScale = Vector3.one * 1.6f;
                speed = 12f;
                break;
            case 5:
                damage = 60;
                maxDistance = 15f;
                transform.localScale = Vector3.one * 2f;
                speed = 15f;
                break;
        }
    }

    IEnumerator Throw()
    {
        // OUTGOING
        while (Vector2.Distance(startPos, transform.position) < maxDistance)
        {
            transform.position += (Vector3)(startDir * speed * Time.deltaTime);
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // spin
            yield return null;
        }

        // RETURNING
        while (Vector2.Distance(transform.position, player.transform.position) > 0.1f)
        {
            Vector2 returnDir = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(returnDir * speed * Time.deltaTime);
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime); // spin
            yield return null;
        }

        // Reset for next throw
        startPos = player.transform.position;
        startDir = SnapDirection(player.lastMoveDir);
        transform.position = startPos;

        StartCoroutine(Throw());
    }

    Vector2 SnapDirection(Vector2 dir)
    {
        if (dir == Vector2.zero)
            return Vector2.down;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 45f) * 45f;

        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
