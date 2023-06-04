using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blobAttack : MonoBehaviour
{
    public int damage = 10;

    private void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Player_Stats playerStats = collision.gameObject.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.TomarDaño(damage);
            }

            Destroy(gameObject); // Destroy the bullet projectile
        }
        else if (collision.CompareTag("Terrain"))
        {
            Destroy(gameObject); // Destroy the bullet projectile
        }
    }
}