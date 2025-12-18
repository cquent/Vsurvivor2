using UnityEngine;
using System.Collections;

public class ZoneSpawner : MonoBehaviour
{
    public int level;
    public GameObject zonePrefab;
    public PlayerMovements player;

    private float spawnCooldown;   // Time between spawning zones
    private float zoneDuration;    // How long each zone lasts

    void Start()
    {
        if (level < 1 || zonePrefab == null || player == null)
            return;

        UpdateStats();
        StartCoroutine(SpawnZonesLoop());
    }

    void UpdateStats()
    {
        // Level affects cooldown and zone duration
        switch (level)
        {
            case 1: spawnCooldown = 10f; zoneDuration = 1f; break;
            case 2: spawnCooldown = 9f; zoneDuration = 1.2f; break;
            case 3: spawnCooldown = 8f; zoneDuration = 1.4f; break;
            case 4: spawnCooldown = 6f; zoneDuration = 1.6f; break;
            case 5: spawnCooldown = 5f; zoneDuration = 2f; break;
        }
    }

    IEnumerator SpawnZonesLoop()
    {
        while (true)
        {
            // Spawn a zone at the player's position
            GameObject zone = Instantiate(zonePrefab, player.transform.position, Quaternion.identity);
            Zone z = zone.GetComponent<Zone>();
            z.level = level;
            z.player = player;
            z.duration = zoneDuration;

            yield return new WaitForSeconds(spawnCooldown);
        }
    }
}
