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
    private void Start()
    {
        PlayerGameObject = GameObject.Find("Player");
        PlayerHitbox = PlayerGameObject.GetComponent<PolygonCollider2D>();
        PlayerCollider = PlayerGameObject.GetComponent<BoxCollider2D>();
        if (NewStats.MeleeSlimeDamage == 0) NewStats.MeleeSlimeDamage = dañoGolpe;
        else dañoGolpe = NewStats.MeleeSlimeDamage;
    }
    void Update()
  {
    var playerRender = PlayerGameObject.GetComponent<Renderer>();
    if (tiempoSiguienteAtaque > 0)
    { 
      tiempoSiguienteAtaque -= Time.deltaTime;
    }

    
  }

  public float getDamage(){
    return dañoGolpe;
  }

  
}
