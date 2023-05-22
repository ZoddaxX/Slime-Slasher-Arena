using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField] private Transform controladorGolpe;
  [SerializeField] private float radioGolpe;
  [SerializeField] private float dañoGolpe;
  [SerializeField] private float tiempoEntreAtaques;
  [SerializeField] private float tiempoSiguienteAtaque;

    // Update is called once per frame
    void Update()
    {
      if (tiempoSiguienteAtaque > 0)
      {
        tiempoSiguienteAtaque -= Time.deltaTime;
      }

      if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0){
       Golpe();
       tiempoSiguienteAtaque = tiempoEntreAtaques;
      }
    }

    private void Golpe(){
      Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

      foreach (Collider2D colisionador in objetos)
      {
          if (colisionador.CompareTag("Enemigo")){
            colisionador.transform.GetComponent<Slime_Stats>().TomarDaño(dañoGolpe);
          }
      }
    }
  

  void OnDrawGizmos(){
  Gizmos.color = Color.red;

  Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
  }
}
