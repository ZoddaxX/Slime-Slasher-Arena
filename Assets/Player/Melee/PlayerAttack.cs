using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float heavyKnockbackMultiplier;
    public GameObject attack;
    public PlayerController playerController;
    public GameObject Player;
    public float tiempoEntreAtaqueLigero;
    public float tiempoEntreAtaquePesado;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpeLigero;
    [SerializeField] private float danoGolpePesado;

    private float tiempoSiguienteAtaque;
    private bool flag = true;
    private Camera mainCam;
    private Vector3 mousePos;

    private void Start()
    {
        tiempoSiguienteAtaque = 0f;
        playerController = Player.GetComponent<PlayerController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
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
        if ( (Input.GetKey("x") || Input.GetMouseButtonDown(0)) && tiempoSiguienteAtaque <= 0 && flag) // GetMouseButtonDown(0)
        {
        GolpeLigero();
        StartCoroutine(DrawAttack(0.1f));
        Debug.Log("atk ligero");
        tiempoSiguienteAtaque = tiempoEntreAtaqueLigero;
        flag = false;
        }
        //Ataque pesado
        if ((Input.GetKey("c") || Input.GetMouseButtonDown(1))&& tiempoSiguienteAtaque <= 0 && flag){ // GetMouseButtonDown(1)
        GolpePesado();
        StartCoroutine(DrawAttack(0.2f));
        Debug.Log("atk fuerte");
        tiempoSiguienteAtaque = tiempoEntreAtaquePesado;
        flag = false;
        }

    float orientation = Mathf.Sign(Player.transform.localScale.x);

    mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

    Vector3 rotation = mousePos - transform.position;

    float rotZ = Mathf.Atan2(rotation.y * orientation, rotation.x * orientation) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.Euler(0, 0, rotZ);


  }

    private void GolpeLigero(){
      Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

      foreach (Collider2D colisionador in objetos)
      {
        if (colisionador.CompareTag("Enemigo")){
          colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpeLigero, 1);
        }
        else if (colisionador.CompareTag("Jefe")){
          colisionador.transform.GetComponent<Boss_Stats>().TomarDano(danoGolpeLigero, 1);
        }
      }
    }

    private void GolpePesado()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpePesado, heavyKnockbackMultiplier);
            }
            else if (colisionador.CompareTag("Jefe")){
                colisionador.transform.GetComponent<Boss_Stats>().TomarDano(danoGolpePesado, heavyKnockbackMultiplier);
            }
        }
      }
    
    IEnumerator DrawAttack(float timer)
    {
        attack.SetActive(true);
        yield return new WaitForSeconds(timer);
        attack.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}