using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 1.5f;
    public float spawnOffset = 2f; // distance hors caméra
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideCamera();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
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
