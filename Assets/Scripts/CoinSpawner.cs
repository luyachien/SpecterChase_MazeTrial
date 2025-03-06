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
    public LayerMask groundLayer;   // 地板層，確保金幣生成在地面上
    public float minY = 1.387957f;  // 金幣最低的 Y 座標
    public float coinSpacing = 1.5f; // 金幣之間的最小距離

    private List<Vector3> spawnedPositions = new List<Vector3>(); // 記錄已放置的位置

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int spawned = 0;
        int maxAttempts = 1000; // 增加最大嘗試次數，確保足夠空間放置金幣

        while (spawned < coinCount && maxAttempts > 0)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minSpawnPos.x, maxSpawnPos.x),
                maxSpawnPos.y + 2.0f, // 讓金幣先生成在較高處
                Random.Range(minSpawnPos.z, maxSpawnPos.z)
            );

            if (IsValidPosition(randomPos))
            {
                // 使用 Raycast 檢查地面
                if (Physics.Raycast(randomPos, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
                {
                    // 計算最終的 Y 座標
                    float finalY = Mathf.Max(hit.point.y + 0.2f, minY);
                    randomPos.y = finalY;

                    // 生成金幣
                    GameObject coin = Instantiate(coinPrefab, randomPos, Quaternion.identity);
                    spawnedPositions.Add(randomPos);

                    // 確保金幣不會穿地
                    if (coin.GetComponent<Rigidbody>() != null)
                    {
                        coin.GetComponent<Rigidbody>().isKinematic = true;
                        coin.GetComponent<Rigidbody>().useGravity = false;
                    }

                    spawned++;
                }
            }
            maxAttempts--;
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        float checkRadius = 0.5f; // 增大檢查範圍，確保金幣不會靠近牆壁
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, obstacleLayer);

        // 如果與牆壁重疊，則無效
        if (colliders.Length > 0)
        {
            return false;
        }

        // 檢查與其他金幣的距離
        foreach (Vector3 spawnedPos in spawnedPositions)
        {
            if (Vector3.Distance(position, spawnedPos) < coinSpacing)
            {
                return false; // 與其他金幣太近，不可放置
            }
        }

        return true;
    }
}
