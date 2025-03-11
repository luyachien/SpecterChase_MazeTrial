using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    public Transform[] patrolPoints;
    private NavMeshAgent agent;
    private int currentPointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogWarning("No patrol points assigned to " + gameObject.name);
            return;
        }

        agent.SetDestination(patrolPoints[currentPointIndex].position);
    }

    void Update()
    {
        if (agent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
    }
}
