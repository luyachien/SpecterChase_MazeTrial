using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // �����w�s��
    public int coinCount = 50;    // �ݭn�ͦ��������ƶq
    public Vector3 minSpawnPos;   // �g�c�d��̤p�� (X, Y, Z)
    public Vector3 maxSpawnPos;   // �g�c�d��̤j�� (X, Y, Z)
    public LayerMask obstacleLayer; // ��ê���h�]�קK�����ͦ��b��W�^

    private List<Vector3> spawnedPositions = new List<Vector3>(); // �O���w��m����m

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        int spawned = 0;
        int maxAttempts = 500;

        while (spawned < coinCount && maxAttempts > 0)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(minSpawnPos.x, maxSpawnPos.x),
                minSpawnPos.y,
                Random.Range(minSpawnPos.z, maxSpawnPos.z)
            );

            if (IsValidPosition(randomPos))
            {
                Instantiate(coinPrefab, randomPos, Quaternion.identity);
                spawnedPositions.Add(randomPos);
                spawned++;
            }
            maxAttempts--;
        }
    }

    bool IsValidPosition(Vector3 position)
    {
        float checkRadius = 0.3f; // �Y�p�ˬd�d��A�קK�L�׭���
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, obstacleLayer);

        return colliders.Length == 0;
    }
}
