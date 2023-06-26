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

  [SerializeField] private float health = 10;
  private Rigidbody2D rb;
  private float healthPercentage;
  private float initialHealth;
  private AudioSource audioSource;

    private void Start()
    {
        //Load rigidbody
        rb = GetComponent<Rigidbody2D>();
        healthPercentage = 1;
        initialHealth = health;
        rangedAttack = GetComponent<RangedAttack>();
        audioSource = GetComponent<AudioSource>();
    }
    public void TomarDano(float damage, float multiplier)
  {
    health -= damage;
    healthPercentage = health * 100 / initialHealth;
    
    if (healthPercentage <= 40)
    {
      Debug.Log("TOY ENOJAO >:D");
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
        spriteColor.a = 0.5f; // Set the desired alpha value (0.0f to 1.0f)
        spriteRenderer.color = spriteColor;

        // Delayed destruction
        float destroyDelay = 2f; 
        Destroy(gameObject, destroyDelay);
    }
}

