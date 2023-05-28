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

    private bool onFloor;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rigidBody;
    private Vector2 velocidad;
    private bool canJump;
    private bool isCrouching;
    private bool canSlide;
    private float slideTimerCool;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        canJump = false;
        isCrouching = false;
        canSlide = true;
        slideTimerCool = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D rayo = Physics2D.Raycast(transform.position, Vector2.down, distanciaRayo, Platform);
        if (rayo.collider != null)
        {
            onFloor = true;
        }
        else
        {
            onFloor = false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    void FixedUpdate()
    {
        if (isSliding)
        {
            return;
        }

        if (onFloor && !canJump && vertical == 0)
        {
            canJump = true;
        }
        // Movimiento
        if (horizontal != 0f && !isCrouching)
        {
            if (Mathf.Abs(rigidBody.velocity.x) <= velHorizontalMax-2 && !isSliding)
            {
                rigidBody.AddForce(new Vector2(horizontal * velHorizontalJugador, 0), ForceMode2D.Impulse);
                
            }
            

            else if (Mathf.Abs(rigidBody.velocity.x) >= velHorizontalMax-2 && !isSliding)
            {
                rigidBody.velocity = Vector2.right * velHorizontalMax * horizontal + Vector2.up * rigidBody.velocity.y;
            }
        }
        // Saltar
        if (vertical > 0f && onFloor && canJump)
        {
            rigidBody.AddForce(new Vector2(0, vertical * velVerticalJugador), ForceMode2D.Impulse);
            canJump = false;
            Debug.Log("Saltando");
        }
        // Agacharse
        if (vertical < 0 && !isCrouching)
        {
            isCrouching = true;
            Debug.Log("Agachado");
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 0.5f);

        }

        else if (vertical >= 0 && isCrouching)
        {
            isCrouching = false;
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 2f);
        }
        // Slide
        if (isCrouching && onFloor && !isSliding && horizontal != 0)
        {
            if (slideTimerCool > slideCooldown && canSlide)
            {
                StartCoroutine(SlideRoutine()); 
            }
            else slideTimerCool +=Time.deltaTime;
        }
        if ((horizontal > 0 && !facingRight) | (horizontal < 0 && facingRight))
        {
            Turn();
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
        canSlide = false;
        isSliding = true;
        rigidBody.velocity = new Vector2(horizontal * 2* velSlide, 0f);
        Debug.Log("start slide");
        yield return new WaitForSeconds(slideTime);
        isSliding = false;
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
        Debug.Log("end slide");
    }
}

