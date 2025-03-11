using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class GhostAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private List<Vector3> waypoints = new List<Vector3>();
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // 確保 waypoints 不是空的，才設定目標點
        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }

    void Update()
    {
        // 如果 waypoints 為空，就不執行 Update
        if (waypoints.Count == 0) return;

        // 當鬼魂抵達當前目標點，移動到下一個點
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            agent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }

    public void SetWaypoints(List<Vector3> newWaypoints)
    {
        if (newWaypoints == null || newWaypoints.Count == 0)
        {
            Debug.LogError("SetWaypoints: 鬼魂的 waypoints 為空！");
            return;
        }

        waypoints = newWaypoints;
        currentWaypointIndex = 0;

        if (waypoints.Count > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }
}
