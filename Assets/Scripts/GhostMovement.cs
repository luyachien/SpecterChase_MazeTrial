using UnityEngine;
using UnityEngine.AI;

public class GhostMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.1f && waypoints != null && waypoints.Length > 0)
        {
            MoveToNextWaypoint();
        }
    }

    public void SetWaypoints(Vector3[] points)
    {
        waypoints = points;
        currentWaypointIndex = 0; // 重置為第一個路徑點
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex]);
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // 循環路徑點
        agent.SetDestination(waypoints[currentWaypointIndex]);
    }
}
