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
    public float velDesSlide;
    public float slideTime;
    public float slideCooldown;


    private bool onFloor;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rigidBody;
    private Vector2 velocidad;
    private bool jumpButton;
    private bool crouch;
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
        if (onFloor && !jumpButton && vertical == 0)
        {
            jumpButton = true;
        }

        if (horizontal != 0f && !isSliding)
        {
            rigidBody.AddForce(new Vector2(horizontal * velHorizontalJugador, 0), ForceMode2D.Impulse);
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
            if (slideTimerCool > slideCooldown)
            {
                isSliding = true;
            //rigidBody.AddForce() = new Vector2(horizontal * velSlide, 0);
            StartCoroutine(SlideRoutine()); 
            }
            else slideTimerCool +=Time.deltaTime;
        }


        velocidad = rigidBody.velocity;
        velocidad.x = Mathf.Clamp(velocidad.x, -velHorizontalMax, velHorizontalMax);
        rigidBody.velocity = velocidad;
    }

    IEnumerator SlideRoutine()     
    {
        float slideTimer = 0;
        float velocidad_slide = rigidBody.velocity.x;
        bool Sliding = true;
        Debug.Log("start slide");
        while (velocidad_slide != 0 || Sliding) 
        {
            slideTimer += Time.deltaTime;
            Sliding = false;
            velocidad_slide = rigidBody.velocity.x;
            if (slideTimer < slideTime) rigidBody.AddForce(Vector2.right * horizontal * velSlide, ForceMode2D.Force);
            else if (velocidad_slide != 0) rigidBody.AddForce(Vector2.left * horizontal * velDesSlide, ForceMode2D.Force);
            Debug.Log("sliding");
            yield return null;
        }
        isSliding = false;
        slideTimer = Time.deltaTime;
        Debug.Log("end slide");
    }
}

