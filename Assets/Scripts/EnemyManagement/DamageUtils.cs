using UnityEngine;
public static class DamageUtils
{
    public static void DamageEnemiesInRadius(
        Vector2 center,
        float radius,
        float damage,
        bool destroyOnHit = false,
        GameObject self = null)
    {
        EnemyHealth[] enemies = GameObject.FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);


        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(center, enemy.transform.position);
            if (dist <= radius)
            {
                enemy.TakeDamage(damage);

                if (destroyOnHit && self != null)
                    Object.Destroy(self);
            }
        }
    }
}
