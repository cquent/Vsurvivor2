using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public float repulsionStrength = 1.5f;

    [Header("Sprite Direction")]
    public bool facingRightByDefault = false; // false = slime, true = bat

    private Transform player;
    private EnemyCollision enemyCollision;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        enemyCollision = GetComponent<EnemyCollision>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        Vector2 toPlayer = (player.position - transform.position).normalized;
        Vector2 repulsion = enemyCollision != null ? enemyCollision.Repulsion : Vector2.zero;

        Vector2 finalMove = toPlayer * speed + repulsion * repulsionStrength;

        // Déplacement
        transform.position += (Vector3)(finalMove * Time.deltaTime);

        // Forcer Z = 0
        Vector3 pos = transform.position;
        pos.z = 0f;
        transform.position = pos;

        Debug.Log(name + " pos: " + transform.position);

        // Flip sprite universel
        if (facingRightByDefault)
            spriteRenderer.flipX = toPlayer.x < 0; // inverse pour ceux qui regardent à droite par défaut
        else
            spriteRenderer.flipX = toPlayer.x > 0; // normal pour ceux qui regardent à gauche
    }
}
