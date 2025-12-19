using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float maxDistance = 50f;
    public float hitRadius = 1f; 

    public Vector2 direction;
    private Vector2 startPos;

    public void Initialize(Vector2 dir, float dmg, float spd, float distance)
    {
        direction = dir.normalized;
        damage = dmg;
        speed = spd;
        maxDistance = distance;
        startPos = transform.position;
    }

    void Update()
    {

        transform.position += (Vector3)(direction * speed * Time.deltaTime);


        if (Vector2.Distance(startPos, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
            return;
        }


        EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);
        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist <= hitRadius)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
    }


}
