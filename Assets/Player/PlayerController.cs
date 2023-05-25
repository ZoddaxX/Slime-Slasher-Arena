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


    private bool onFloor;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rigidBody;
    private Vector2 velocidad;
    private bool jumpButton;
    private bool crouch;
    private bool canSlide = true;
    private bool isSliding;
    private float slideTimerCool;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpButton = false;
        crouch = false;
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
        if (onFloor && !jumpButton && vertical == 0)
        {
            jumpButton = true;
        }

        if (horizontal != 0f && !crouch)
        {
            Debug.Log(rigidBody.velocity.x);
            if (Mathf.Abs(rigidBody.velocity.x) <= velHorizontalMax-2 && !isSliding)
            {
                rigidBody.AddForce(new Vector2(horizontal * velHorizontalJugador, 0), ForceMode2D.Impulse);
                
            }
            

            else if (Mathf.Abs(rigidBody.velocity.x) >= velHorizontalMax-2 && !isSliding)
            {
                rigidBody.velocity = Vector2.right * velHorizontalMax * horizontal + Vector2.up * rigidBody.velocity.y;
            }
        }

        if (vertical != 0f && onFloor && jumpButton)
        {
            rigidBody.AddForce(new Vector2(0, vertical * velVerticalJugador), ForceMode2D.Impulse);
            jumpButton = false;
            Debug.Log("Estas saltando");
        }

        if (vertical < 0 && !crouch)
        {
            crouch = true;
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 0.5f);

        }

        else if (vertical >= 0 && crouch)
        {
            crouch = false;
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * 2f);
        }
        if (crouch && onFloor && !isSliding && horizontal != 0)
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

