using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
     public Vector2 lastMoveDir = Vector2.down;
    [Header("Movement")]
    public float speed = 5f; 
    public InputActionReference moveAction;
    
    [Header("Animation")]
    public Animator animator; 
    private Vector2 movement;

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
        
    }
}
