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

    private float lastJump = 0;
    private Rigidbody2D slimeRB;
    private bool sentido;
    private float player_x;
    private float player_y;
    private float slime_x;
    private float slime_y;
    private double distancia;

    // Start is called before the first frame update
    void Start()
    {
        sentido = true;
    }

    // Update is called once per frame
    void Update()
    {
        player_x = player.transform.position.x;
        player_y = player.transform.position.y;
        slime_x = transform.position.x;
        slime_y = transform.position.y;
        slimeRB = GetComponent<Rigidbody2D>();

        distancia = Mathf.Pow(Mathf.Pow(slime_x - player_x, 2) + Mathf.Pow(slime_y - player_y, 2),0.5f);
        
        if (Time.time - lastJump > jumpCooldown)
        {
            Debug.Log("Slime Salta");
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
}
