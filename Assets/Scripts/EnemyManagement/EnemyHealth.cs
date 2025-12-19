using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;

    void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
