using UnityEngine;
using UnityEngine.AI;

public class RandomPathPoints : MonoBehaviour
{
    public int numberOfPoints = 5; // 路徑點數量
    public float range = 10f; // 隨機範圍
    private Vector3[] pathPoints;

    void Start()
    {
        pathPoints = new Vector3[numberOfPoints];
        GenerateRandomPathPoints();
    }

    void GenerateRandomPathPoints()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            Vector3 randomPoint = Random.insideUnitSphere * range;
            randomPoint.y = 0; // 確保Y軸為0，以便在地面上

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                pathPoints[i] = hit.position; // 儲存可行走的路徑點
            }
        }
    }

    public Vector3[] GetPathPoints()
    {
        return pathPoints;
    }
}
