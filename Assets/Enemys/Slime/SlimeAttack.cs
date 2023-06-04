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
  public BoxCollider2D SlimeCollider;
  public GameObject SlimeGameObject;
  public PolygonCollider2D PlayerHitbox;
  public BoxCollider2D PlayerCollider;
  public GameObject PlayerGameObject;

  // Update is called once per frame
  void Update()
  {
    var playerRender = PlayerGameObject.GetComponent<Renderer>();
    if (tiempoSiguienteAtaque > 0)
    { 
      tiempoSiguienteAtaque -= Time.deltaTime;
    }

    if (SlimeCollider.IsTouching(PlayerHitbox)){
      Debug.Log("player recibe daño");
      Golpe();
      tiempoSiguienteAtaque = tiempoEntreAtaque;
    }
    /*
    if (tiempoSiguienteAtaque != tiempoEntreAtaque){
      if ( (tiempoEntreAtaque - 0.3) > tiempoSiguienteAtaque) playerRender.material.SetColor("_Color", Color.red);
    }
    /*
    if (Input.GetKeyDown(KeyCode.Q)){
      PlayerCollider.transform.GetComponent<Player_Stats>().TomarDaño(dañoGolpe);
    }
    */
  }

  private void Golpe(){
    var playerRender = PlayerGameObject.GetComponent<Renderer>();
    PlayerCollider.transform.GetComponent<Player_Stats>().TomarDano(dañoGolpe);
    if ( (tiempoSiguienteAtaque) > (tiempoEntreAtaque - 0.3f)){
      playerRender.material.SetColor("_Color", Color.red);
    }
    playerRender.material.SetColor("_Color", Color.white);
  }

  /*
  void OnDrawGizmos(){
  Gizmos.color = Color.red;

  Gizmos.DrawWireCube(controladorGolpe.position, controladorGolpe.Scale);
  }
  */ 
  /*
  void OnTriggerEnter2D(Collider2D other){
    Debug.Log("entra al ontriggerenter");
    if (other.tag == "Jugador"){
      Player.transform.GetComponent<Player_Stats>().TomarDaño(dañoGolpe);
      Debug.Log("entra al if");
    }
  }
  */
}
