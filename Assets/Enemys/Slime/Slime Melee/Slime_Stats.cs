using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Stats : MonoBehaviour
{
    public Transform jugador;
    public bool alive = true;
    public float fuerzaKnockback;
    public float alturaKnockback;
    public AudioClip audioHit1;
    public AudioClip audioHit2;
    public AudioClip audioHit3;
    public AudioClip audioDeath;
    public Animator animator;
    public GameObject Corazon;
    public GameObject blood;

    [SerializeField] private float health = 10;
    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Start()
    {
        //Load rigidbody
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.Find("Player").transform;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
        if (NewStats.MeleeSlimeHealth == 0) NewStats.MeleeSlimeHealth = health; 
        else health = NewStats.MeleeSlimeHealth;
    }
    public void TomarDano(float damage, float multiplier)
  {
    health -= damage;

    if (health <= 0)
    {
      Death(rb);
      alive = false;
    } else {
      int audio = Random.Range(1,4);
      if (audio == 1) audioSource.clip = audioHit1;
      else if (audio == 2) audioSource.clip = audioHit2;
      else audioSource.clip = audioHit3;
      audioSource.Play();

      Vector2 direccion = transform.position - jugador.position;
      direccion.Normalize();
      direccion += alturaKnockback * Vector2.up;
      rb.AddForce(direccion * fuerzaKnockback * multiplier, ForceMode2D.Impulse);
      StartCoroutine(bleed());
    }
  }

  private void Death(Rigidbody2D rb)
  {
        rb.simulated = false;
        ControladorSonido.Instance.ReproducirSonido(audioDeath);
        animator.SetBool("dead", true);

        // Get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Change the transparency of the sprite
        Color spriteColor = spriteRenderer.color;
        //spriteColor.a = 0.5f; // Set the desired alpha value (0.0f to 1.0f)
        spriteRenderer.color = spriteColor;
        int valor = Random.Range(1,6);
        if (valor == 1) Instantiate(Corazon, gameObject.transform.position, Quaternion.identity);

        // Delayed destruction
        float destroyDelay = 2f; 
        Destroy(gameObject, destroyDelay);
    }

private IEnumerator bleed()
    {
        blood.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        blood.SetActive(false);
    }
}

