using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostManager : MonoBehaviour
{
    public GameObject ghostPrefab; // ����w�s��
    public int minGhosts = 3; // �ְ̤���ƶq
    public int maxGhosts = 5; // �̦h����ƶq
    public int minPatrolPoints = 3; // �C������̤֨����I��
    public int maxPatrolPoints = 5; // �C������̦h�����I��
    public Vector3 mazeBoundsMin; // �g�c�d��̤p��
    public Vector3 mazeBoundsMax; // �g�c�d��̤j��
    public LayerMask groundLayer; // �a�O�ϼh�A�T�O�����I�i�樫

    private List<GameObject> ghosts = new List<GameObject>(); // �s��ͦ�������

    void Start()
    {
        int ghostCount = Random.Range(minGhosts, maxGhosts + 1); // �H���M�w����ƶq
        for (int i = 0; i < ghostCount; i++)
        {
            SpawnGhost();
        }
    }

    void SpawnGhost()
    {
        Vector3 spawnPosition = GetRandomPosition(); // �H�����Ͱ����m
        GameObject ghost = Instantiate(ghostPrefab, spawnPosition, Quaternion.identity);
        ghosts.Add(ghost);

        GhostController ghostController = ghost.GetComponent<GhostController>();
        ghostController.patrolPoints = GeneratePatrolRoute(); // ���t�H�����޸��u
    }

    Vector3 GetRandomPosition()
    {
        for (int i = 0; i < 10; i++) // ���� 10 ���A�T�O���X�z����m
        {
            float x = Random.Range(mazeBoundsMin.x, mazeBoundsMax.x);
            float z = Random.Range(mazeBoundsMin.z, mazeBoundsMax.z);
            Vector3 position = new Vector3(x, 1, z); // y=1�A�קK����i�a�O

            // �ˬd�o�Ӧ�m�O�_�i�樫
            if (Physics.Raycast(position + Vector3.up * 2, Vector3.down, 3f, groundLayer))
            {
                return position;
            }
        }
        return mazeBoundsMin; // �䤣��X�A��m�ɡA��^�g�c���
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
