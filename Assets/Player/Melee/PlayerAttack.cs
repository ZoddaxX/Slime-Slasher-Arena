using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
  public int heavyKnockbackMultiplyer;
  public GameObject attack;

  [SerializeField] private Transform controladorGolpe;
  [SerializeField] private float radioGolpe;
  [SerializeField] private float danoGolpeLigero;
  [SerializeField] private float danoGolpePesado;
  [SerializeField] private float tiempoEntreAtaqueLigero;
  [SerializeField] private float tiempoEntreAtaquePesado;
  
  private PlayerController playerController;
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
    if (Input.GetKey("x") && tiempoSiguienteAtaque <= 0 && flag) // GetMouseButtonDown(0)
    {
      GolpeLigero();
      StartCoroutine(DrawAttack(0.1f));
      Debug.Log("atk ligero");
      tiempoSiguienteAtaque = tiempoEntreAtaqueLigero;
      flag = false;
    }
    //Ataque pesado
    if (Input.GetKey("c") && tiempoSiguienteAtaque <= 0 && flag){ // GetMouseButtonDown(1)
      GolpePesado();
      StartCoroutine(DrawAttack(0.2f));
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
          colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpeLigero, 1);
        }
      }
    }
    
    private void GolpePesado(){
      Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

      foreach (Collider2D colisionador in objetos)
      {
        if (colisionador.CompareTag("Enemigo")){
          colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpePesado, heavyKnockbackMultiplyer);
        }
      }
    }
    IEnumerator DrawAttack(float timer)
    {
        attack.SetActive(true);
        yield return new WaitForSeconds(timer);
        attack.SetActive(false);
    }

  void OnDrawGizmos(){
  Gizmos.color = Color.red;

  Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
  }
}
