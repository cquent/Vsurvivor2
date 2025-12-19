using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public int level = 1;
    public float currentXP = 0f;
    public float baseXP = 50f;       // XP nécessaire pour passer du niveau 1 au 2
    public float xpGrowth = 1.25f;   // Multiplicateur de l’XP requise par niveau
    public int maxLevel = 25;

    public float XPForNextLevel => baseXP * Mathf.Pow(xpGrowth, level - 1);

    public void GainXP(float amount)
    {
        if (level >= maxLevel) return;

        currentXP += amount;
        Debug.Log($"Gained {amount} XP! Current XP: {currentXP}/{XPForNextLevel}");

        while (currentXP >= XPForNextLevel && level < maxLevel)
        {
            currentXP -= XPForNextLevel;
            LevelUp();
        }
    }

    void LevelUp()
    {
        level++;
        Debug.Log($"Level Up! New Level: {level}");

    }
}
