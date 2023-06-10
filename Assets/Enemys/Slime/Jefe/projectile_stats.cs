using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_stats : MonoBehaviour
{
    public float damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            Destroy(gameObject); // Destroy the bullet projectile
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject); // Destroy the bullet projectile
        }
    }

    public float getDamage(){
        return damage;
    }
}