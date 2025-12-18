using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public float maxDistance;

    public Vector2 direction;
    public Vector2 startPos;

    // Called by Gun immediately after Instantiate
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
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"Bullet hit {other.name} for {damage} damage");
            Destroy(gameObject);
        }
    }
}
