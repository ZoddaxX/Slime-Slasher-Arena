using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    PlayerController playerController;
  [SerializeField] private Transform controladorGolpe;
  [SerializeField] private float radioGolpe;
  [SerializeField] private float dañoGolpeLigero;
  [SerializeField] private float dañoGolpePesado;
  [SerializeField] private float tiempoEntreAtaqueLigero;
  [SerializeField] private float tiempoEntreAtaquePesado;
  private float tiempoSiguienteAtaque;
  private bool flag = true;
    private void Start()
    {
        tiempoSiguienteAtaque = 0f;
        playerController = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
  {
    //No atacar cuando esta en slide
    if(playerController.isSliding)
    {
      return;
    }
    //
    if (tiempoSiguienteAtaque > 0)
    { 
      flag = true;
      tiempoSiguienteAtaque -= Time.deltaTime;
    }
    //Ataque ligero
    if (Input.GetMouseButtonDown(0) && tiempoSiguienteAtaque <= 0 && flag)
    {
      GolpeLigero();
      Debug.Log("atk ligero");
      tiempoSiguienteAtaque = tiempoEntreAtaqueLigero;
      flag = false;
    }
    //Ataque pesado
    if (Input.GetMouseButtonDown(1) && tiempoSiguienteAtaque <= 0 && flag){
      GolpePesado();
      Debug.Log("atk fuerte");
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
