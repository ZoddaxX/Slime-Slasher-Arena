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
    private bool onFloor;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rigidBody;
    private Vector2 velocidad;
    private bool jumpButton;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        jumpButton = false;
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

        if (horizontal != 0f)
        {
            rigidBody.AddForce(new Vector2(horizontal * velHorizontalJugador, 0), ForceMode2D.Impulse);
        } 

        if (vertical != 0f && onFloor && jumpButton)
        {
            rigidBody.AddForce(new Vector2(0, vertical * velVerticalJugador), ForceMode2D.Impulse);
            jumpButton = false;
            Debug.Log("Estas saltando");
        }

        velocidad = rigidBody.velocity;
        velocidad.x = Mathf.Clamp(velocidad.x, -velHorizontalMax, velHorizontalMax);
        rigidBody.velocity = velocidad;
    }    
}

