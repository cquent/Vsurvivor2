using UnityEngine;
using System.Collections;

public class Zone : MonoBehaviour
{
    public int level;

    [Header("Damage Settings")]
    public float damage = 5f;
    public float attackRate = 0.5f;   // How often it applies damage
    public float radius = 4f;

    [Header("Duration")]
    public float duration = 1f;       // How long this instance lasts

    public PlayerMovements player;
    public SpriteRenderer sprite;

    void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (level < 1)
        {
            gameObject.SetActive(false);
            return;
        }

        UpdateStats();
        StartCoroutine(ZoneEffect());
    }

    void UpdateStats()
    {
        switch (level)
        {
            case 1: damage = 10f; radius = 4f; attackRate = 0.5f; break;
            case 2: damage = 15f; radius = 6f; attackRate = 0.45f; break;
            case 3: damage = 20f; radius = 8f; attackRate = 0.4f; break;
            case 4: damage = 22f; radius = 9f; attackRate = 0.35f; break;
            case 5: damage = 25f; radius = 10f; attackRate = 0.3f; break;
        }

        transform.localScale = Vector3.one * radius * 2f;
    }

    IEnumerator ZoneEffect()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Keep zone centered on player
            if (player != null)
                transform.position = player.transform.position;

            // Damage enemies in zone using EnemyHealth components (no collider needed)
            EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
            foreach (var enemy in enemies)
            {
                float dist = Vector2.Distance(transform.position, enemy.transform.position);
                if (dist <= radius)
                {
                    enemy.TakeDamage(damage);
                }
            }

            elapsed += attackRate;
            yield return new WaitForSeconds(attackRate);
        }

        Destroy(gameObject);
    }
}
