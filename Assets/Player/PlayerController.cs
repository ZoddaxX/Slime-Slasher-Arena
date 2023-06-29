using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float distanciaRayo;
    public float velHorizontalJugador;
    public float velVerticalJugador;
    public float velHorizontalMax;
    public LayerMask Platform;
    public float velSlide;
    public float slideTime;
    public float slideCooldown;
    public bool facingRight = true;
    public bool isSliding;
    public float alturaKnockback;
    public float fuerzaKnockback;
    public float invisTimer;
    public AudioClip audioSlide;
    public AudioClip audioWalk;
    public Animator animator;

    [SerializeField] private TrailRenderer trail;
    private bool onFloor;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rigidBody;
    private bool canJump;
    private bool isCrouching;
    private bool canSlide;
    private float slideTimerCool;
    private bool onKnockback;
    private bool canKnockback;
    private Camera mainCam;
    private float mousePosition;
    private AudioSource audioSource;
    private bool walkSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        canJump = false;
        isCrouching = false;
        canSlide = true;
        slideTimerCool = 10f;
        onKnockback = false;
        canKnockback = true;
        PolygonCollider2D playerCollider = GetComponent<PolygonCollider2D>();
        Debug.Log(("El ancho es", playerCollider.bounds.size.x, "y el largo es de", playerCollider.bounds.size.y));
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioSource = GetComponent<AudioSource>();
        walkSound = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayo = Physics2D.Raycast(transform.position, Vector2.down, distanciaRayo, Platform);
        if (rayo.collider != null)
        {
            onFloor = true;
            animator.SetBool("grounded", true);
        }
        else
        {
            onFloor = false;
            animator.SetBool("grounded", false);
        }
        if (rigidBody.velocity.y == 0) animator.SetBool("jump", false);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        animator.SetFloat("airSpeed", rigidBody.velocity.y);
        animator.SetFloat("facing", transform.localScale.x * rigidBody.velocity.x);
        if (isSliding)
        {
            return;
        }

        if (onFloor && !canJump && vertical == 0)
        {
            canJump = true;
        }
        // Movimiento
        if (horizontal != 0f && !isCrouching && !onKnockback)
        {
            animator.SetBool("walking", true);
            trail.emitting = false;
            if (!walkSound && onFloor)
            {
                audioSource.clip = audioWalk;
                audioSource.Play();
                walkSound = true;
            }
            else if (!audioSource.isPlaying && onFloor)
            {
                audioSource.clip = audioWalk;
                audioSource.Play();
            }

            if (!onFloor) audioSource.Stop();

            if (Mathf.Abs(rigidBody.velocity.x) <= velHorizontalMax-2 && !isSliding && !onKnockback)
            {
                rigidBody.AddForce(new Vector2(horizontal * velHorizontalJugador, 0), ForceMode2D.Impulse);
                
            }
            

            else if (Mathf.Abs(rigidBody.velocity.x) >= velHorizontalMax-2 && !isSliding && !onKnockback)
            {
                rigidBody.velocity = Vector2.right * velHorizontalMax * horizontal + Vector2.up * rigidBody.velocity.y;
            }
        }
        else if (walkSound)
        {
            audioSource.Stop();
            walkSound = false;
        }
        else animator.SetBool("walking", false);
        // Saltar
        if (vertical > 0f && onFloor && canJump)
        {
            animator.SetBool("jump", true);
            rigidBody.AddForce(new Vector2(0, vertical * velVerticalJugador), ForceMode2D.Impulse);
            canJump = false;
            trail.emitting = false;
            Debug.Log("Saltando");
        }
        // Agacharse
        if (vertical < 0 && !isCrouching && onFloor)
        {
            isCrouching = true;
            Debug.Log("Agachado");
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 0.5f);
            animator.SetBool("crouch", true);

        }

        else if (vertical >= 0 && isCrouching)
        {
            isCrouching = false;
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 2f);
            animator.SetBool("crouch", false);
        }
        // Slide
        if (isCrouching && onFloor && !isSliding && horizontal != 0 && !onKnockback)
        {
            if (slideTimerCool > slideCooldown && canSlide)
            {
                StartCoroutine(SlideRoutine()); 
            }
            else slideTimerCool +=Time.deltaTime;
        }

        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition).x;

        if ( (gameObject.transform.position.x - mousePosition < 0 && !facingRight) || (gameObject.transform.position.x - mousePosition >= 0 && facingRight))
        {
            Turn();
        }

        if (rigidBody.velocity.x == 0 && rigidBody.velocity.y == 0)
        {
            audioSource.Stop();
        }

    }

    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        facingRight = !facingRight;
    }


    IEnumerator SlideRoutine()     
    {
        animator.SetBool("sliding", true);
        audioSource.clip = audioSlide;
        audioSource.Play();
        canSlide = false;
        isSliding = true;
        trail.emitting = true;
        rigidBody.velocity = new Vector2(horizontal * 2* velSlide, 0f);
        Debug.Log("start slide");
        yield return new WaitForSeconds(slideTime);
        isSliding = false;
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
        trail.emitting = false;
        audioSource.Stop();
        Debug.Log("end slide");
        animator.SetBool("sliding", false);
    }

    IEnumerator invisCooldown(){
        yield return new WaitForSeconds(invisTimer);
        canKnockback = true; 
    }

    IEnumerator isOnKnockback(){
        yield return new WaitForSeconds(0.5f);
        while (!onFloor){
            yield return 0;
        }
        onKnockback = false;
        Debug.Log("PUEDES MOVERTE");
        yield return 0;
    }

    public void PlayerTrigger(Collider2D enemigo, float daño){
        if ((enemigo.gameObject.CompareTag("Enemigo") || enemigo.gameObject.CompareTag("Jefe") || enemigo.gameObject.CompareTag("Projectile"))  && canKnockback)
        {
            canKnockback = false;
            onKnockback = true;
            Vector2 direccion = transform.position - enemigo.gameObject.transform.position;
            direccion.Normalize();
            direccion += alturaKnockback * Vector2.up;
            rigidBody.AddForce(new Vector2(rigidBody.velocity.x * -1, rigidBody.velocity.y * -1), ForceMode2D.Impulse);
            rigidBody.AddForce(direccion * fuerzaKnockback, ForceMode2D.Impulse);
            GetComponent<Player_Stats>().TomarDano(daño);

            StartCoroutine(invisCooldown());
            StartCoroutine(isOnKnockback());
        }
    }
}

