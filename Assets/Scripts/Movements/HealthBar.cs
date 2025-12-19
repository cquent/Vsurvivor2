using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Vector3 originalScale;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void SetHealth(float current, float max)
    {
        float ratio = current / max;

        // Lock X scale to always be positive so health bar doesn't flip with player
        transform.localScale = new Vector3(
            Mathf.Abs(originalScale.x) * ratio,
            originalScale.y,
            originalScale.z
        );
    }
}
