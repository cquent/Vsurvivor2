using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;
    public float xpReward = 10f;

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
        PlayerXP playerXP = FindFirstObjectByType<PlayerXP>();
        if (playerXP != null)
        {
            // XP peut être proportionnelle au HP max de l’ennemi si tu veux
            float reward = maxHP / 20 * xpReward; // ou reward = maxHP * someFactor
            playerXP.GainXP(reward);
        }
        Destroy(gameObject);
    }
}