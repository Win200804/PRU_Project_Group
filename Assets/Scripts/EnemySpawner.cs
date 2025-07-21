using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class EnemyType
{
    public GameObject prefab; // Prefab của loại quái
    public int count;         // Số lượng quái loại này
}

[System.Serializable]
public class Wave
{
    public List<EnemyType> enemies; // Danh sách các loại quái và số lượng tương ứng
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private List<Wave> waves; // Danh sách các wave

    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            Wave currentWave = waves[currentWaveIndex];
            Debug.Log($"🔄 Starting Wave {currentWaveIndex + 1}");

            foreach (EnemyType enemyType in currentWave.enemies)
            {
                for (int i = 0; i < enemyType.count; i++)
                {
                    SpawnEnemy(enemyType.prefab);
                    yield return new WaitForSeconds(spawnInterval);
                }
            }

            Debug.Log($"✅ Finished Wave {currentWaveIndex + 1}");

            currentWaveIndex++;
            if (currentWaveIndex < waves.Count)
            {
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }

        Debug.Log("🏁 All waves completed!");
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}
