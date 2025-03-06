using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrefab; // 鬼魂的預製體
    public int minGhosts = 3; // 最少鬼魂數量
    public int maxGhosts = 5; // 最多鬼魂數量
    public int minPatrolPoints = 3; // 每隻鬼魂最少巡邏點數
    public int maxPatrolPoints = 5; // 每隻鬼魂最多巡邏點數
    public Vector3 mazeBoundsMin; // 迷宮範圍最小值
    public Vector3 mazeBoundsMax; // 迷宮範圍最大值
    public LayerMask groundLayer; // 地板圖層，確保巡邏點可行走

    private List<GameObject> ghosts = new List<GameObject>(); // 存放生成的鬼魂

    void Start()
    {
        int ghostCount = Random.Range(minGhosts, maxGhosts + 1); // 隨機決定鬼魂數量
        for (int i = 0; i < ghostCount; i++)
        {
            SpawnGhost();
        }
    }

    void SpawnGhost()
    {
        Vector3 spawnPosition = GetRandomPosition(); // 隨機產生鬼魂的位置
        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghosts.Add(ghost);

        GhostController ghostController = ghost.GetComponent<GhostController>();
        ghostController.patrolPoints = GeneratePatrolRoute(); // 分配隨機巡邏路線
    }

    Vector3 GetRandomPosition()
    {
        for (int i = 0; i < 10; i++) // 嘗試 10 次，確保找到合理的位置
        {
            float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
            float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
            Vector3 position = new Vector3(x, 1, z); // y=1，避免鬼魂掉進地板

            // 檢查這個位置是否可行走
            if (Physics.Raycast(position + Vector3.up * 2, Vector3.down, 3f, groundLayer))
            {
                return position;
            }
        }
        return mazeBoundsMin; // 找不到合適位置時，返回迷宮邊界
    }

    Transform[] GeneratePatrolRoute()
    {
        int patrolCount = Random.Range(minPatrolPoints, maxPatrolPoints + 1);
        Transform[] patrolPoints = new Transform[patrolCount];

        for (int i = 0; i < patrolCount; i++)
        {
            Vector3 patrolPos = GetRandomPosition();
            GameObject patrolPoint = new GameObject("PatrolPoint" + i);
            patrolPoint.transform.position = patrolPos;
            patrolPoints[i] = patrolPoint.transform;
        }

        return patrolPoints;
    }
}
