using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Point : MonoBehaviour
{
    public float AddLife;

    public float getAddLife(){
        return AddLife;
    }
    void OnTriggerEnter2D(Collider2D colision){
        Debug.Log("RAR");
        if (colision.CompareTag("Jugador")){
            Player_Stats player = colision.GetComponent<Player_Stats>();
            Debug.Log("Trigger");
            player.moreHealth(AddLife);
            Destroy(this);
        }
    }
}
