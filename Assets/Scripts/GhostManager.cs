using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrefab; 
    public int minGhosts = 3;
    public int maxGhosts = 5;
    public int minPatrolPoints = 3;
    public int maxPatrolPoints = 5;
    public Vector3 mazeBoundsMin;
    public Vector3 mazeBoundsMax;
    public LayerMask groundLayer;
    public float navMeshSampleDistance = 2.0f; // NavMesh範圍檢測距離

    private List<GameObject> ghosts = new List<GameObject>();

    void Start()
    {
        int ghostCount = Random.Range(minGhosts, maxGhosts + 1);
        for (int i = 0; i < ghostCount; i++)
        {
            SpawnGhost();
        }

        if (ghostPrefab != null)
        {
            Destroy(ghostPrefab);
        }
    }

    void SpawnGhost()
    {
        Vector3 spawnPosition = GetValidNavMeshPosition();
        if (spawnPosition == Vector3.zero) return; // 如果沒找到可行走位置，就不生成

        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghosts.Add(ghost);

        GhostController ghostController = ghost.GetComponent<GhostController>();
        ghostController.patrolPoints = GeneratePatrolRoute();
    }

    Vector3 GetValidNavMeshPosition()
    {
        for (int i = 0; i < 10; i++) // 最多嘗試 10 次
        {
            float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
            float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
            Vector3 randomPosition = new Vector3(x, 1, z);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
            {
                return hit.position; // 返回可行走區域的點
            }
        }
        Debug.LogWarning("Failed to find a valid NavMesh position.");
        return Vector3.zero;
    }

    Transform[] GeneratePatrolRoute()
    {
        int patrolCount = Random.Range(minPatrolPoints, maxPatrolPoints + 1);
        Transform[] patrolPoints = new Transform[patrolCount];

        for (int i = 0; i < patrolCount; i++)
        {
            Vector3 patrolPos = GetValidNavMeshPosition();
            if (patrolPos == Vector3.zero) continue; 

            GameObject patrolPoint = new GameObject("PatrolPoint" + i);
            patrolPoint.transform.position = patrolPos;
            patrolPoints[i] = patrolPoint.transform;
        }

        return patrolPoints;
    }
}
