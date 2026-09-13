using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int totalEnemies = 10;

    List<GameObject> enemies = new List<GameObject>();

    int enemiesKilled = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogWarning("Player prefab not assigned in GameManager.");
            return;
        }

        Instantiate(playerPrefab, spawnPoints[0].position, Quaternion.identity);
    }

    void SpawnEnemies()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("Enemy prefab not assigned in GameManager.");
            return;
        }

        for (int i = 0; i < totalEnemies; i++)
        {
            Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
            var e = Instantiate(enemyPrefab, sp.position, Quaternion.identity);
            enemies.Add(e);
        }
    }

    public void OnEnemyKilled()
    {
        enemiesKilled++;
        Debug.Log($"Enemies killed: {enemiesKilled}");
    }
}
