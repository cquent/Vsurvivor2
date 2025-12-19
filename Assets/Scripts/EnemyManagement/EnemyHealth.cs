using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public float maxHP = 10f;
    private float currentHP;

    [Header("Death")]
    public float corpseDuration = 1.2f;

    private bool isDead = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private EnemyMovement movement;

    void Awake()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<EnemyMovement>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHP -= damage;

        // Flash rouge clair
        if (sprite != null)
            StartCoroutine(HitFlash());

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        // XP
        PlayerXP playerXP = FindAnyObjectByType<PlayerXP>();
        if (playerXP != null)
            playerXP.GainXP(maxHP);

        // Stop movement
        if (movement != null)
            movement.enabled = false;

        // Animation de mort
        if (animator != null)
            animator.SetTrigger("Die");
        
  
        // Destruction après délai
        Destroy(gameObject, 1f + corpseDuration);
    }

    IEnumerator HitFlash()
    {
        Color original = sprite.color;
        sprite.color = new Color(1f, 0.5f, 0.5f, 1f)
;
        yield return new WaitForSeconds(0.08f);
        sprite.color = original;
    }
}
