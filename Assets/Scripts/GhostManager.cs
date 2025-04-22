using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrefab;
    public int minGhosts = 5;
    public int maxGhosts = 8;
    public int minPatrolPoints = 3;
    public int maxPatrolPoints = 5;
    public Vector3 mazeBoundsMin;
    public Vector3 mazeBoundsMax;
    public LayerMask obstacleLayer;
    public float navMeshSampleDistance = 3.0f;
    public float wallCheckDistance = 2.0f;

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
        Vector3 spawnPosition = GetValidSpawnPosition();
        if (spawnPosition == Vector3.zero) return;

        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghosts.Add(ghost);

        GhostController ghostController = ghost.GetComponent<GhostController>();
        ghostController.patrolPoints = GeneratePatrolRoute();
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
            float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
            Vector3 randomPosition = new Vector3(x, 1, z);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
            {
                Vector3 finalPosition = hit.position;
                finalPosition.y += 1;

                if (!IsNearWall(finalPosition) && IsNavMeshWalkable(finalPosition))
                {
                    return finalPosition;
                }
            }
        }
        Debug.LogWarning("Failed to find a valid spawn position.");
        return Vector3.zero;
    }

    bool IsNearWall(Vector3 position)
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(position, dir, wallCheckDistance, obstacleLayer))
            {
                return true;
            }
        }
        return false;
    }

    bool IsNavMeshWalkable(Vector3 position)
    {
        NavMeshHit hit;
        return !NavMesh.Raycast(position, position + Vector3.down * 2, out hit, NavMesh.AllAreas);
    }

    Transform[] GeneratePatrolRoute()
    {
        int patrolCount = Random.Range(minPatrolPoints, maxPatrolPoints + 1);
        Transform[] patrolPoints = new Transform[patrolCount];

        for (int i = 0; i < patrolCount; i++)
        {
            Vector3 patrolPos = GetValidSpawnPosition();
            if (patrolPos == Vector3.zero) continue;

            GameObject patrolPoint = new GameObject("PatrolPoint" + i);
            patrolPoint.transform.position = patrolPos;
            patrolPoints[i] = patrolPoint.transform;
        }

        return patrolPoints;
    }
}
