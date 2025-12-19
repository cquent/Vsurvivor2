using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float baseHP = 10f;     // base value
    public float maxHP;
    private float currentHP;
    public float xpReward = 10f;

    public void Initialize(float healthMultiplier)
    {
        maxHP = baseHP * healthMultiplier;
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
        PlayerXP playerXP = FindFirstObjectByType<PlayerXP>();
        if (playerXP != null)
        {
            float reward = maxHP / 20f * xpReward;
            playerXP.GainXP(reward);
        }

        Destroy(gameObject);
    }
}
