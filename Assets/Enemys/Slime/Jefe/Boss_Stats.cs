using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Stats : MonoBehaviour
{
  public Transform jugador;
  public bool alive = true;
  public float fuerzaKnockback;
  public float alturaKnockback;
  public RangedAttack rangedAttack;
  public AudioClip audioHit1;
  public AudioClip audioHit2;
  public AudioClip audioHit3;
  public AudioClip audioDeath;
  public GameObject Corazon;
  public Animator animator;
  public float healthPercentage;
  public float health = 40;

  private Rigidbody2D rb;
  private float initialHealth;
  private AudioSource audioSource;

    private void Start()
    {
        //Load rigidbody
        rb = GetComponent<Rigidbody2D>();
        rangedAttack = GetComponent<RangedAttack>();
        audioSource = gameObject.AddComponent<AudioSource>();
        healthPercentage = 1;
        if (NewStats.BossSlimeHealth == 0) NewStats.BossSlimeHealth = health;
        else health = NewStats.BossSlimeHealth;
        initialHealth = health;
    }
    public void TomarDano(float damage, float multiplier)
  {
    health -= damage;
    healthPercentage = health / initialHealth;
    
    if (healthPercentage <= 0.4f)
    {
      rangedAttack.rageMode();
    }
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
    }
  }

  private void Death(Rigidbody2D rb)
  {
        rb.simulated = false;
        ControladorSonido.Instance.ReproducirSonido(audioDeath);

        // Get the sprite renderer component
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Change the transparency of the sprite
        Color spriteColor = spriteRenderer.color;
        spriteRenderer.color = spriteColor;
        Instantiate(Corazon, gameObject.transform.position, Quaternion.identity);
        Instantiate(Corazon, gameObject.transform.position, Quaternion.identity);
        Instantiate(Corazon, gameObject.transform.position, Quaternion.identity);

        animator.SetBool("dead", true);
        // Delayed destruction
        float destroyDelay = 2f; 
        Destroy(gameObject, destroyDelay);
    }
}

