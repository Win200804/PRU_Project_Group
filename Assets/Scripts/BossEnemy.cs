using UnityEditor.Tilemaps;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float bulletNormalSpeed = 20f;
    [SerializeField] private float bulletSpecialSpeed = 10f;
    [SerializeField] private float hpValue = 100f;
    [SerializeField] private GameObject miniEnemyPrefab;
    [SerializeField] private float skillCooldown = 5f;
    private float nextskillTimer= 5f;
    [SerializeField] private GameObject usbPrefabs;
    protected override void Update()
    {
        base.Update();
        if(Time.time >= nextskillTimer)     
        {
            UseSkills();
        }
    }
    protected override void Die()
    {
        Instantiate(usbPrefabs, transform.position, Quaternion.identity);
        base.Die();
    }
    private void OntrierEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.TakeDamage(enterDamage);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {   
        if (collision.CompareTag("Player"))
        {   
            player.TakeDamage(stayDamage);
        }
    }
    private void NormalSkill()
    {
        if (player != null)
        {
            Vector3 directionToPlayer =  player.transform.position - bulletSpawnPoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, bulletSpawnPoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * bulletNormalSpeed);
        }
    }
    private void SpecialSkill()
    {
        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletdirection = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            GameObject bullet = Instantiate(bulletPrefabs, bulletSpawnPoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletdirection * bulletSpecialSpeed);
        }
    }
    private void HealingSkill(float hpAmmount)
    {
        currentHp= Mathf.Min(currentHp + hpAmmount, maxHp);
        UpdateHpBar();
    }
    private void SpawnMiniEnemy()
    {
        Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);
    }
    private void Teleport()
    {
        if(player != null)
        {
            transform.position = player.transform.position;
        }
    }
    private void ChooseRandomSkill() 
    {
        int randomSkill = Random.Range(0, 5);
        switch(randomSkill)
        {
            case 0:
                NormalSkill();
                break;
            case 1:
                SpecialSkill();
                break;
            case 2:
                HealingSkill(hpValue);
                break;
            case 3:
                SpawnMiniEnemy();
                break;
            case 4:
                Teleport();
                break;
        }
    }
    private void UseSkills()
    {
        nextskillTimer = Time.time + skillCooldown;
        ChooseRandomSkill();
    }
}
