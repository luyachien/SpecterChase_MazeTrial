using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("❌ 鬼魂缺少 NavMeshAgent，請確認已添加！");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("❌ 鬼魂不在 NavMesh 上，請確認位置是否正確！");
            return;
        }

        agent.autoBraking = false;
        MoveToNextPoint();
    }

    void Update()
    {
        if (agent == null || !agent.isOnNavMesh)
            return;

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint();
        }
    }

    void MoveToNextPoint()
    {
        if (patrolPoints.Length == 0)
            return;

        agent.destination = patrolPoints[currentPointIndex].position;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
    }
}
