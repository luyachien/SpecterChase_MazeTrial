using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // 金幣預製體
    public int coinCount = 50;    // 需要生成的金幣數量
    public Vector3 minSpawnPos;   // 迷宮範圍最小值 (X, Y, Z)
    public Vector3 maxSpawnPos;   // 迷宮範圍最大值 (X, Y, Z)
    public LayerMask obstacleLayer; // 障礙物層（避免金幣生成在牆上）

    private List<Vector3> spawnedPositions = new List<Vector3>(); // 記錄已放置的位置

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int spawned = 0;
        int maxAttempts = 500;

        while (spawned < coinCount && maxAttempts > 0)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minSpawnPos.x, maxSpawnPos.x),
                minSpawnPos.y,
                Random.Range(minSpawnPos.z, maxSpawnPos.z)
            );

            if (IsValidPosition(randomPos))
            {
                Instantiate(coinPrefab, randomPos, Quaternion.identity);
                spawnedPositions.Add(randomPos);
                spawned++;
            }
            maxAttempts--;
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        float checkRadius = 0.3f; // 縮小檢查範圍，避免過度限制
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, obstacleLayer);

        return colliders.Length == 0;
    }
}
