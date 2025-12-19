using UnityEngine;
using System.Collections;

public class EnnemySpawner : MonoBehaviour
{
    public System.Collections.Generic.List<GameObject> enemyPrefab;
    public GameObject player;
    public float spawnInterval = 1.5f;
    public float spawnOffset = 2f; // distance hors caméra
    public Camera mainCamera;
    public float EnemyHealthMultiplier = 1f;
    private int level;

    private GameObject enemyChosen;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideCamera();
        level = player.GetComponent<PlayerXP>().level;
        if (level >= 25)
        {
            spawnInterval = 0.5f;
            EnemyHealthMultiplier = 4f;
        }
        else if (level >= 20)
        {
            spawnInterval = 0.75f;
            EnemyHealthMultiplier = 3f;
        }
        else if (level >= 15)
        {
            spawnInterval = 0.1f;
            EnemyHealthMultiplier = 2.5f;
        }
        else if (level >= 10)
        {
            spawnInterval = 0.15f;
            EnemyHealthMultiplier = 2f;
        }
        else if (level >= 5)
        {
            spawnInterval = 0.2f;
            EnemyHealthMultiplier = 1.5f;
        }
        else
        {
            spawnInterval = 0.25f;
        }
        if (level >= 15)
        {
            enemyChosen = enemyPrefab[Random.Range(0, enemyPrefab.Count)];
        }
        else
        {
            if (level % 3 == 0)
            {
                enemyChosen = enemyPrefab[1];
            }
            else
            {
                enemyChosen = enemyPrefab[0];
            }
        }
        if (level % 5 == 0)
        {
            enemyChosen = enemyPrefab[1];
        }

        GameObject enemy = Instantiate(enemyChosen, spawnPos, Quaternion.identity);

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.Initialize(EnemyHealthMultiplier);
        }

    }

    Vector3 GetSpawnPositionOutsideCamera()
    {
        float camHeight = mainCamera.orthographicSize * 2f;
        float camWidth = camHeight * mainCamera.aspect;

        // Choisir un côté aléatoire
        int side = Random.Range(0, 4);

        Vector3 pos = Vector3.zero;

        switch (side)
        {
            case 0: // haut
                pos = new Vector3(
                    Random.Range(-camWidth / 2f, camWidth / 2f),
                    camHeight / 2f + spawnOffset,
                    0f
                );
                break;

            case 1: // bas
                pos = new Vector3(
                    Random.Range(-camWidth / 2f, camWidth / 2f),
                    -camHeight / 2f - spawnOffset,
                    0f
                );
                break;

            case 2: // droite
                pos = new Vector3(
                    camWidth / 2f + spawnOffset,
                    Random.Range(-camHeight / 2f, camHeight / 2f),
                    0f
                );
                break;

            case 3: // gauche
                pos = new Vector3(
                    -camWidth / 2f - spawnOffset,
                    Random.Range(-camHeight / 2f, camHeight / 2f),
                    0f
                );
                break;
        }

        // Convertir de l'espace caméra vers le monde
        return mainCamera.transform.position + pos;
    }
}
