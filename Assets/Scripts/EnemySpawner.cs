using UnityEngine;
using System.Collections;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 5f;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }
    private IEnumerator  SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject enemy = enemies[Random.Range(0, enemies.Length)];
            Instantiate(enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }

}
