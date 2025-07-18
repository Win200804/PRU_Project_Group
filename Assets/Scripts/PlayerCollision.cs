using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(20);
        }
        else if (collision.CompareTag("Usb"))
        {
            Debug.Log("YOU WIN!");
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
        }
    }
}
