using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_SlimeMelee : MonoBehaviour
{
    public GameObject player;
    public float agro_Range;
    public int velHorizontal;
    public int velSalto;
    public float jumpCooldown;
    public LayerMask Plataform;
    public Animator animator;
    public AudioClip audioGround;
    

    Slime_Stats slimeStats;
    private float lastJump = 0;
    private Rigidbody2D slimeRB;
    private bool sentido;
    private float player_x;
    private float player_y;
    private float slime_x;
    private float slime_y;
    private double distancia;
    private bool canJump = true;
    private float timeOnGround = 0;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private bool esVisible;
    

    // Start is called before the first frame update
    void Start()
    {
        sentido = true;
        player = GameObject.Find("Player");
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        spriteRenderer =  GetComponent<SpriteRenderer>();
        esVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        player_x = player.transform.position.x;
        player_y = player.transform.position.y;
        slime_x = transform.position.x;
        slime_y = transform.position.y;
        slimeRB = GetComponent<Rigidbody2D>();
        slimeStats = GetComponent<Slime_Stats>();

        distancia = Mathf.Pow(Mathf.Pow(slime_x - player_x, 2) + Mathf.Pow(slime_y - player_y, 2),0.5f);
        
        if (!slimeStats.alive)
        {
            return;
        }

        BoxCollider2D slimeCollider = GetComponent<BoxCollider2D>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, slimeCollider.bounds.size.y / 2 + 0.1f, Plataform);
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position - new Vector3(slimeCollider.bounds.size.x / 2, 0, 0), Vector2.down, slimeCollider.bounds.size.y / 2 + 0.1f, Plataform);
        RaycastHit2D hitright = Physics2D.Raycast(transform.position + new Vector3(slimeCollider.bounds.size.x / 2, 0, 0), Vector2.down, slimeCollider.bounds.size.y / 2 + 0.1f, Plataform);
        Debug.DrawRay(transform.position - new Vector3(slimeCollider.bounds.size.x / 2, 0, 0), Vector2.down * slimeCollider.bounds.size.y / 2 + new Vector2(0, -0.1f), Color.red);
        if (hit.collider != null || hitleft.collider != null || hitright.collider != null)
        {
            canJump = true;
            animator.SetBool("jump", false);
            if (timeOnGround == 0 && esVisible)
            {
                audioSource.clip = audioGround;
                audioSource.Play();
            }
            timeOnGround += Time.deltaTime;
        }
        else
        {
            canJump = false;
            timeOnGround = 0;
            animator.SetBool("jump", true);

        }

        if (Time.time - lastJump > jumpCooldown && timeOnGround > jumpCooldown && canJump)
        {
            if (distancia <= agro_Range)
            {
                slime_agro();
            }

            else
            {
                jump(sentido, velHorizontal, velSalto);

                if (sentido)
                {
                    sentido = false;
                }

                else
                {
                    sentido = true;
                }
            }

            lastJump = Time.time;
            canJump = false;
            timeOnGround = 0;
         
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == Plataform
            && !canJump && timeOnGround > jumpCooldown)
        {
            canJump = true;
        }
    }
    void jump(bool sentido_s, int velHorizontal, int velSalto)
    {
        if (sentido_s)
        {
            slimeRB.AddForce(new Vector2(velHorizontal, velSalto), ForceMode2D.Impulse);
        }
        else
        {
            slimeRB.AddForce(new Vector2(-velHorizontal, velSalto), ForceMode2D.Impulse);
        }
        spriteRenderer.flipX = !sentido_s;
    }

    void slime_agro()
    {
        if (gameObject.transform.position.x > player_x)
        {
            jump(false, velHorizontal, velSalto);
        } 

        else
        {
            jump(true, velHorizontal, velSalto);
        }
    }

    private void OnBecameVisible(){
        esVisible = true;
    }

    private void OnBecameInvisible(){
        esVisible = false;
    }
}
