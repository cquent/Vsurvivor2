using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour
{
    public float damage;
    public float speed = 75f; // fast projectile
    public float length = 5f; // visual length
    public float duration = 0.5f; // max lifetime
    public float hitRadius = 1.5f; // rayon pour toucher les ennemis

    private Vector2 direction;
    private PlayerMovements player;
    private float elapsed = 0f;

    public void Initialize(int lvl, PlayerMovements p, Vector2 dir)
    {
        player = p;
        direction = dir.normalized;

        UpdateStats(lvl);
        RotateAndScale();
    }

    void UpdateStats(int lvl)
    {
        switch (lvl)
        {
            case 1: damage = 10; length = 5; duration = 0.3f; break;
            case 2: damage = 15; length = 6; duration = 0.35f; break;
            case 3: damage = 25; length = 8; duration = 0.4f; break;
            case 4: damage = 40; length = 10; duration = 0.45f; break;
            case 5: damage = 60; length = 12; duration = 0.5f; break;
        }
    }

    void RotateAndScale()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Stretch sprite to match visual length
        transform.localScale = new Vector3(length, transform.localScale.y, 1f);
    }

    void Update()
    {
        elapsed += Time.deltaTime;


        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= hitRadius)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Lazer hit {enemy.name} for {damage} damage");
            }
        }

        if (elapsed >= duration)
            Destroy(gameObject);
    }

  
}
