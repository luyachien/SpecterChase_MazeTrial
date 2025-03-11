using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public Transform[] spawnPoints;
    public int numberOfGhosts = 3;
    public int waypointCount = 10;
    public float minDistanceBetweenWaypoints = 3f;

    private List<Vector3> waypoints = new List<Vector3>();

    void Start()
    {
        // 刪除所有場景中的鬼魂
        GameObject[] existingGhosts = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (GameObject ghost in existingGhosts)
        {
            Destroy(ghost);
        }

        // 確保 ghostPrefab 不影響場景
        if (ghostPrefab != null)
        {
            ghostPrefab.SetActive(false);
        }
        else
        {
            Debug.LogError("GhostSpawner: ghostPrefab 未設置！");
            return;
        }

        // 檢查 spawnPoints 是否有設定
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("SpawnGhost: spawnPoints 陣列是空的！請在 Inspector 設定鬼魂的生成點！");
            return;
        }

        GenerateWaypoints();

        // 確保 waypoints 至少有 3 個
        if (waypoints.Count < 3)
        {
            Debug.LogError("生成的 waypoints 太少（少於 3 個），請檢查迷宮 NavMesh 是否正常！");
            return;
        }

        SpawnGhosts();
    }

    void GenerateWaypoints()
    {
        waypoints.Clear();
        int maxAttempts = waypointCount * 5;

        for (int i = 0; i < waypointCount; i++)
        {
            int attempts = 0;
            Vector3 randomPoint;

            do
            {
                float randomX = Random.Range(-35f, 38f);
                float randomZ = Random.Range(-50f, 45f);
                randomPoint = new Vector3(randomX, 1f, randomZ);

                NavMeshHit hit;
                bool isOnNavMesh = NavMesh.SamplePosition(randomPoint, out hit, 2.0f, NavMesh.AllAreas);

                bool isFarEnough = true;
                foreach (Vector3 existingPoint in waypoints)
                {
                    if (Vector3.Distance(existingPoint, hit.position) < minDistanceBetweenWaypoints)
                    {
                        isFarEnough = false;
                        break;
                    }
                }

                if (isOnNavMesh && isFarEnough)
                {
                    waypoints.Add(hit.position);
                    break;
                }

                attempts++;
            } while (attempts < maxAttempts);
        }

        Debug.Log("成功生成 waypoints：" + waypoints.Count);
    }

    void SpawnGhosts()
    {
        for (int i = 0; i < numberOfGhosts; i++)
        {
            SpawnGhost();
        }
    }

    void SpawnGhost()
    {
        if (waypoints.Count < 3)
        {
            Debug.LogError("SpawnGhost: waypoints 太少（少於 3 個），無法產生鬼魂的移動路線！");
            return;
        }

        // 生成隨機位置
        Vector3 randomSpawnPos;
        NavMeshHit hit;
        int attempts = 0;
        bool foundValidPosition = false;

        do
        {
            float randomX = Random.Range(-35f, 38f);
            float randomZ = Random.Range(-50f, 45f);
            randomSpawnPos = new Vector3(randomX, 1f, randomZ);

            if (NavMesh.SamplePosition(randomSpawnPos, out hit, 2.0f, NavMesh.AllAreas))
            {
                foundValidPosition = true;
                randomSpawnPos = hit.position;
            }

            attempts++;
        } while (!foundValidPosition && attempts < 10);

        if (!foundValidPosition)
        {
            Debug.LogError("SpawnGhost: 找不到合適的 NavMesh 位置來生成鬼魂！");
            return;
        }

        // 生成鬼魂（確保它是啟用的）
        GameObject newGhost = Instantiate(ghostPrefab, randomSpawnPos, Quaternion.identity);
        newGhost.SetActive(true); // **確保 Clone 出來的鬼魂是可見的**

        GhostAI ghostAI = newGhost.GetComponent<GhostAI>();

        int pathLength = Random.Range(3, Mathf.Min(6, waypoints.Count));
        List<Vector3> selectedWaypoints = new List<Vector3>();

        for (int i = 0; i < pathLength; i++)
        {
            int randomIndex = Random.Range(0, waypoints.Count);
            selectedWaypoints.Add(waypoints[randomIndex]);
        }

        ghostAI.SetWaypoints(selectedWaypoints);
    }
}
