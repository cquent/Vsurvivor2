using UnityEngine;
using System.Collections.Generic;

public class EnemyCollision : MonoBehaviour
{
    public float minDistance = 0.6f;

    static List<EnemyCollision> enemies = new List<EnemyCollision>();

    public Vector2 Repulsion { get; private set; }

    void OnEnable()
    {
        enemies.Add(this);
    }

    void OnDisable()
    {
        enemies.Remove(this);
    }

    void Update()
    {
        Vector2 push = Vector2.zero;

        foreach (var other in enemies)
        {
            if (other == this) continue;

            Vector2 diff = (Vector2)(transform.position - other.transform.position);
            float dist = diff.magnitude;

            if (dist > 0f && dist < minDistance)
            {
                push += diff.normalized * (minDistance - dist);
            }
        }

        Repulsion = push;
    }
}
