using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentEnergy = 100;
    [SerializeField] private int energyThreshold = 3;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject enemySpawner;
    private bool bossSpawned = false;
    void Start()
    {
        boss.SetActive(false);
    }

    
    void Update()
    {
        
    }
    public void AddEnergy()
    {
        if (bossSpawned)
        {
            return;
        }
        currentEnergy += 1;
        if(currentEnergy >= energyThreshold)
        {
            SpawnBoss();
        }
    }
    public void SpawnBoss()
    {
        bossSpawned = true;
        boss.SetActive(true);
        enemySpawner.SetActive(false);
    }
}
