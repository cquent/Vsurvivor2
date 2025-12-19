using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using System.Collections;

public class PlayerMovements : MonoBehaviour
{
    public Vector2 lastMoveDir = Vector2.down;
    [Header("Movement")]
    public float speed = 5f;
    public InputActionReference moveAction;

    [Header("Animation")]
    public Animator animator;
    private Vector2 movement;

    [Header("Health")]
    public float maxHP = 200f;
    public float currentHP;
    public float regenRate = 1f;
    public float invincibilityTime = 0.5f;

    public HealthBar healthBar;
    private bool invincible;

    [Header("Invincibility Flash")]
    public SpriteRenderer spriteRenderer;
    public float flashInterval = 0.1f;


    void Start()
    {
        currentHP = maxHP;
        healthBar.SetHealth(currentHP, maxHP);

    }


    void Update()
    {
        // Read Input
        movement = moveAction.action.ReadValue<Vector2>();
        // Character Movement
        Vector3 move = new Vector3(movement.x, movement.y, 0f) * speed * Time.deltaTime;
        transform.Translate(move);

        Vector2 input = moveAction.action.ReadValue<Vector2>();
        if (input != Vector2.zero)
        {
            lastMoveDir = input.normalized;
        }

        // Animation

        if (animator != null)
        {
            if (movement.magnitude > 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }

        if (movement.x > 0.01f) // vers la droite
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (movement.x < -0.01f) // vers la gauche
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        Regenerate();

    }
    void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        currentHP = Mathf.Max(0, currentHP);
        healthBar.SetHealth(currentHP, maxHP);
        StartCoroutine(Invincibility());
    }



    IEnumerator Invincibility()
    {
        invincible = true;

        float elapsed = 0f;
        bool visible = true;

        while (elapsed < invincibilityTime)
        {
            visible = !visible;
            spriteRenderer.enabled = visible;


            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval;
        }

        spriteRenderer.enabled = true;
        invincible = false;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ennemy") && !invincible)
        {
            TakeDamage(10f);
        }
    }

    void Regenerate()
    {
        if (currentHP < maxHP)
        {
            currentHP += regenRate * Time.deltaTime;
            currentHP = Mathf.Min(currentHP, maxHP);
            healthBar.SetHealth(currentHP, maxHP);
        }
    }



}