using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public PlayerController playerController;
    void OnTriggerEnter2D(Collider2D enemigo){
        if (enemigo.CompareTag("Enemigo") || enemigo.CompareTag("Jefe") || enemigo.gameObject.CompareTag("Projectile") )
        {
            if (enemigo.gameObject.CompareTag("Projectile") != true)
            {
                playerController.PlayerTrigger(enemigo, enemigo.GetComponent<SlimeAttack>().getDamage());
            }
            else
            {
                playerController.PlayerTrigger(enemigo, enemigo.GetComponent<projectile_stats>().getDamage());
            }
        }
    }
}
