using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Inject] DiContainer container;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 10;
    private List<GameObject> enemyPool = new List<GameObject>();

    [SerializeField] SpawnPointData[] spawnPoints; 
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] int maxActiveEnemies = 5;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = container.InstantiatePrefab(enemyPrefab);
            obj.SetActive(false);
            enemyPool.Add(obj);
        }
        InvokeRepeating(nameof(TrySpawnEnemy), 2f, spawnInterval);
    }

    void TrySpawnEnemy()
    {
        if (GetActiveEnemyCount() >= maxActiveEnemies) return;
        SpawnPointData selectedPoint = null;
        ShuffleSpawnPoints();

        foreach (SpawnPointData spData in spawnPoints)
        {
            if (!IsVisibleByCamera(spData.transform.position))
            {
                selectedPoint = spData;
                break;
            }
        }

        if (selectedPoint != null)
        {
            SpawnFromPool(selectedPoint);
        }
    }

    bool IsVisibleByCamera(Vector3 position)
    {
        Vector3 screenPoint = mainCam.WorldToViewportPoint(position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    void SpawnFromPool(SpawnPointData spawnData)
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                if (NavMesh.SamplePosition(spawnData.transform.position, out NavMeshHit hit, 10f, NavMesh.AllAreas))
                {
                    enemy.transform.position = hit.position;

                    enemy.SetActive(true);

                    NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.enabled = true;
                    }

                    if (enemy.TryGetComponent(out EnemyController controller))
                    {
                        controller.SetPatrolPoints(spawnData.patrolPointsForThisZone);
                    }
                    break;
                }
            }
        }
    }

    int GetActiveEnemyCount()
    {
        int count = 0;
        foreach (var e in enemyPool) if (e.activeInHierarchy) count++;
        return count;
    }

    void ShuffleSpawnPoints()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            SpawnPointData temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Length);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }
}