using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public Vector2 Pointerposition { get; set; }
    public Animator animator;
    public PlayerController playerController;
    public PlayerAttack playerAttack;
    public GameObject Player;

    private Camera mainCam;
    private WeaponRotation weaponPivot;
    private float delay;
    private bool attacking;

    private void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
        
        weaponPivot = GetComponentInChildren<WeaponRotation>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    private void Update()
    {
        Vector2 direction = (Pointerposition - (Vector2)transform.position).normalized;
        Vector2 scale = transform.localScale;
        if(direction.x < 0)
        {
            scale.y = -Mathf.Abs(scale.y);
            scale.x = -Mathf.Abs(scale.x);
        }
        else if (direction.x > 0)
        {
            scale.y = Mathf.Abs(scale.y);
            scale.x = Mathf.Abs(scale.x);
        }
        transform.localScale = scale;
        weaponPivot.Pointerposition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        transform.right = (Pointerposition - (Vector2)transform.position).normalized;

        if ((Input.GetKey("x") || Input.GetMouseButtonDown(0))) // GetMouseButtonDown(0)
        {
            delay = playerAttack.tiempoEntreAtaqueLigero;
            Attack("l");
        }
        //Ataque pesado
        if ((Input.GetKey("c") || Input.GetMouseButtonDown(1)))
        { // GetMouseButtonDown(1)
            delay = playerAttack.tiempoEntreAtaquePesado;
            Attack("h");

        }
    }

    public void Attack(string attkType)
    {
        if (playerController.isSliding || attacking)
        {
            return;
        }
        if(attkType == "l")
        {
            animator.SetTrigger("attack");
        }
        if (attkType == "h")
        {
            animator.SetTrigger("heavy");
        }
        attacking = true;
        StartCoroutine(DelayAttack());
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        attacking = false;
    }
}
