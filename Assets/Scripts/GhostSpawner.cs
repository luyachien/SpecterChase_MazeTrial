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

        Vector3 spawnPosition = Vector3.zero;
        bool foundValidSpawn = false;
        int attempts = 0;

        // 1. 嘗試在 spawnPoints 中選擇有效的位置
        while (attempts < spawnPoints.Length && !foundValidSpawn)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPoint.position, out hit, 1.0f, NavMesh.AllAreas))
            {
                spawnPosition = hit.position;
                foundValidSpawn = true;
            }
            attempts++;
        }

        // 2. 如果 spawnPoints 內沒找到有效位置，再嘗試隨機生成位置
        attempts = 0;
        while (!foundValidSpawn && attempts < 10)
        {
            float randomX = Random.Range(-35f, 38f);
            float randomZ = Random.Range(-50f, 45f);
            Vector3 randomPosition = new Vector3(randomX, 1f, randomZ);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, 2.0f, NavMesh.AllAreas))
            {
                // 3. 確保不與牆壁或其他物件重疊
                if (Physics.OverlapSphere(hit.position, 1.0f).Length == 0)
                {
                    spawnPosition = hit.position;
                    foundValidSpawn = true;
                }
            }
            attempts++;
        }

        if (!foundValidSpawn)
        {
            Debug.LogError("SpawnGhost: 找不到合適的生成位置！");
            return;
        }

        // 4. 生成鬼魂並設定巡邏點
        GameObject newGhost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        newGhost.SetActive(true);

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
