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
            case 1: damage = 5f; radius = 6f; attackRate = 0.5f; break;
            case 2: damage = 8f; radius = 10f; attackRate = 0.45f; break;
            case 3: damage = 12f; radius = 15f; attackRate = 0.4f; break;
            case 4: damage = 18f; radius = 20f; attackRate = 0.35f; break;
            case 5: damage = 25f; radius = 25f; attackRate = 0.3f; break;
        }

        transform.localScale = Vector3.one * radius * 2f;

    }

    IEnumerator ZoneEffect()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {


            // Damage enemies in zone
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    Debug.Log($"Zone hit {hit.name} for {damage} damage");
                    // hit.GetComponent<Enemy>()?.TakeDamage(damage);
                }
            }

            elapsed += attackRate;
            yield return new WaitForSeconds(attackRate);
        }

        Destroy(gameObject);
    }
}
