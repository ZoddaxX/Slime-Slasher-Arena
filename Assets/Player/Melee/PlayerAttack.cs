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
    public AudioClip audioLightAttack;
    public AudioClip audioHeavyAttack;
    public bool attackEmpty = true;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float danoGolpeLigero;
    [SerializeField] private float danoGolpePesado;

    private float tiempoSiguienteAtaque;
    private bool flag = true;
    private Camera mainCam;
    private Vector3 mousePos;
    private AudioSource audioSource;
    private AudioSource audioSourceAux;
    

    private void Start()
    {
        tiempoSiguienteAtaque = 0f;
        playerController = Player.GetComponent<PlayerController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        audioSourceAux = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
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
        //StartCoroutine(DrawAttack(0.1f));
        Debug.Log("atk ligero");
        tiempoSiguienteAtaque = tiempoEntreAtaqueLigero;
        flag = false;
        }
        //Ataque pesado
        if ((Input.GetKey("c") || Input.GetMouseButtonDown(1))&& tiempoSiguienteAtaque <= 0 && flag){ // GetMouseButtonDown(1)
        GolpePesado();
        //StartCoroutine(DrawAttack(0.2f));
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
      if (audioSource.isPlaying)
        {
          audioSourceAux.clip = audioLightAttack;
          audioSourceAux.Play();
        }
        else
        {
          audioSource.clip = audioLightAttack;
          audioSource.Play();
        }

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                    colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpeLigero, 1);
                    attackEmpty = false;
            }
            else if (colisionador.CompareTag("Jefe"))
            {
                    colisionador.transform.GetComponent<Boss_Stats>().TomarDano(danoGolpeLigero, 1);
                    attackEmpty = false;
            }

        }
        if (!attackEmpty)
        {
            StartCoroutine(hitWait(0.07f,"l"));
            attackEmpty = true;
        }
    }

    private void GolpePesado()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        if (audioSource.isPlaying)
        {
          audioSourceAux.clip = audioHeavyAttack;
          audioSourceAux.Play();
        }
        else
        {
          audioSource.clip = audioHeavyAttack;
          audioSource.Play();
        }

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Slime_Stats>().TomarDano(danoGolpePesado, heavyKnockbackMultiplier);
                Debug.Log("attack not empty");
                attackEmpty = false;

            }
            else if (colisionador.CompareTag("Jefe")){
                colisionador.transform.GetComponent<Boss_Stats>().TomarDano(danoGolpePesado, heavyKnockbackMultiplier);
                attackEmpty = false;
            }
        }
        if (!attackEmpty)
        {
            StartCoroutine(hitWait(0.15f,"h"));
            attackEmpty = true;
        }

    }

    IEnumerator hitWait(float duration,string mode)
    {
        float delay = 0f;
        if (mode == "l")
            delay = 0.1f;
        else if (mode == "h")
            delay = 0.2f;
            
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
    

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }

    public void AddLightAttack(){
      danoGolpeLigero++;
    }
    public void AddHeavyAttack(){
      danoGolpePesado++;
    }
    public float GetLightAttack(){
      return danoGolpeLigero;
    }
    public float GetHeavyAttack(){
      return danoGolpePesado;
    }
}