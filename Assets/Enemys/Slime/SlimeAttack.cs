using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonoBehaviour
{
  [SerializeField] private Transform controladorGolpe;
  [SerializeField] private float radioGolpe;
  [SerializeField] private float dañoGolpe;
  [SerializeField] private float tiempoEntreAtaque;
  [SerializeField] private float tiempoSiguienteAtaque;
  public BoxCollider2D Slime;
  public BoxCollider2D Player;

  // Update is called once per frame
  void Update()
  {
    if (tiempoSiguienteAtaque > 0)
    { 
      tiempoSiguienteAtaque -= Time.deltaTime;
    }

    if (Slime.IsTouching(Player) && tiempoSiguienteAtaque <= 0){
      Golpe();
      tiempoSiguienteAtaque = tiempoEntreAtaque;
    }
  }

    private void Golpe(){
      /*
      BoxCollider2D objetos = Player; //Physics2D.OverlapBox(controladorGolpe.position, controladorGolpe.Scale);

      foreach (Collider2D colisionador in objetos)
      {
        if (colisionador.CompareTag("Enemigo")){
          colisionador.transform.GetComponent<Player_Stats>().TomarDaño(dañoGolpe);
        }
      }
      */ 
      if (Player.CompareTag("Jugador")){
        Player.transform.GetComponent<Player_Stats>().TomarDaño(dañoGolpe);
      }
    }
  
    /*
  void OnDrawGizmos(){
  Gizmos.color = Color.red;

  Gizmos.DrawWireCube(controladorGolpe.position, controladorGolpe.Scale);
  }
  */
}
