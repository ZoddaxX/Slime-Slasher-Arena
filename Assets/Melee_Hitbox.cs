using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee_Hitbox : MonoBehaviour
{
    private PlayerController jugador;

    private Transform t_jugador;

    void Start(){
        Debug.Log("SAS");
        jugador = GetComponent<PlayerController>();
        t_jugador = GetComponentInParent<Transform>();
    }
    private void OnTriggerEnter(Collider enemigo){
        Debug.Log("RAR");
        if (enemigo.CompareTag("Enemigo") && jugador.getCanKnockback())
        {
            IEnumerator invisCooldown(){
                float tiempo = 0;
                while (tiempo <= jugador.getInvisTimer())
                {
                    tiempo += Time.deltaTime;
                    yield return 0;
                }
                jugador.newCanKnockback(true);
                yield return 0;
            }
            jugador.newCanKnockback(false);
            jugador.newOnKnockback(true);
            Rigidbody2D rb_jugador = transform.parent.GetComponent<Rigidbody2D>();
            Debug.Log("SAS");
            Vector2 direccion = t_jugador.position - enemigo.gameObject.transform.position;
            direccion.Normalize();
            direccion += jugador.alturaKnockback * Vector2.up;
            rb_jugador.AddForce(direccion * jugador.fuerzaKnockback, ForceMode2D.Impulse);

            StartCoroutine(invisCooldown());

            new WaitForSeconds(0.3f);
            while (!jugador.getOnFloor()){
            }
            jugador.newOnKnockback(false);
        }
    }
}
