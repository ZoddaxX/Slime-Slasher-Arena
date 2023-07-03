using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public PlayerController playerController;
    private AudioSource audioSource;
    public Player_Stats player_stats;
    public AudioClip GetHealth;

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

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
            audioSource.Play();
        }
        else if (enemigo.CompareTag("Collectible"))
        {
            Heart_Point heart = enemigo.GetComponent<Heart_Point>();
            float vida = heart.getAddLife();
            player_stats.moreHealth(vida);
            ControladorSonido.Instance.ReproducirSonido(GetHealth);
            Destroy(enemigo.gameObject);
        }
    }
}
