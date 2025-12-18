using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public int level;
    public float speed = 5f;
    public float maxDistance = 3f;
    public float damage = 20f;
    public float explosionRadius = 1.5f;

    public GameObject explosionPrefab; // Assign a centered explosion prefab

    [HideInInspector] public Vector2 direction;
    [HideInInspector] public Vector2 startPos;

    private bool exploded = false;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(Throw());
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: damage = 20f; explosionRadius = 15f; break;
            case 2: damage = 30f; explosionRadius = 17f; break;
            case 3: damage = 45f; explosionRadius = 20f; break;
            case 4: damage = 60f; explosionRadius = 25f; break;
            case 5: damage = 80f; explosionRadius = 30f; break;
        }
    }

    public void Initialize(Vector2 dir, int lvl)
    {
        direction = dir.normalized;
        level = lvl;
        UpdateStats();
    }

    IEnumerator Throw()
    {
        while (Vector2.Distance(startPos, transform.position) < maxDistance)
        {
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            yield return null;
        }

        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        if (exploded) return;
        exploded = true;

        // Spawn explosion visual
        if (explosionPrefab != null)
        {
            GameObject exp = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            // Scale relative to prefab's original size
            float baseSize = 1f; // set this to the original sprite size if needed
            float scaleFactor = explosionRadius / baseSize;
            exp.transform.localScale = Vector3.one * scaleFactor;

            Destroy(exp, 0.5f); // destroy visual after 0.5s
        }

        // Damage enemies in radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Debug.Log($"Bomb hit {hit.name} for {damage} damage");
                // hit.GetComponent<Enemy>()?.TakeDamage(damage);
            }
        }
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
