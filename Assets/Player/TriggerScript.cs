using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public PlayerController playerController;
    void OnTriggerEnter2D(Collider2D enemigo){
        if (enemigo.CompareTag("Enemigo"))
        {
            playerController.PlayerTrigger(enemigo);
            Debug.Log("RAR1");
        }
    }
}
