using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  [SerializeField] private Transform controladorGolpe;
  [SerializeField] private float radioGolpe;
  [SerializeField] private float dañoGolpeLigero;
  [SerializeField] private float dañoGolpePesado;
  [SerializeField] private float tiempoEntreAtaqueLigero;
  [SerializeField] private float tiempoEntreAtaquePesado;
  [SerializeField] private float tiempoSiguienteAtaque;
  private bool flag = true;

  // Update is called once per frame
  void Update()
  {
    if (tiempoSiguienteAtaque > 0)
    { 
      flag = true;
      tiempoSiguienteAtaque -= Time.deltaTime;
    }

    if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0 && flag){
      GolpeLigero();
      tiempoSiguienteAtaque = tiempoEntreAtaqueLigero;
      flag = false;
    }

    if (Input.GetButtonDown("Fire3") && tiempoSiguienteAtaque <= 0 && flag){
      GolpePesado();
      tiempoSiguienteAtaque = tiempoEntreAtaquePesado;
      flag = false;
    }
  }

    private void GolpeLigero(){
      Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

      foreach (Collider2D colisionador in objetos)
      {
        if (colisionador.CompareTag("Enemigo")){
          colisionador.transform.GetComponent<Slime_Stats>().TomarDaño(dañoGolpeLigero);
        }
      }
    }
    
    private void GolpePesado(){
      Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

      foreach (Collider2D colisionador in objetos)
      {
        if (colisionador.CompareTag("Enemigo")){
          colisionador.transform.GetComponent<Slime_Stats>().TomarDaño(dañoGolpePesado);
        }
      }
    }
  

  void OnDrawGizmos(){
  Gizmos.color = Color.red;

  Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
  }
}
