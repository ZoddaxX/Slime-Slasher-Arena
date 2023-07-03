using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public GameObject blobPrefab;
    public Transform blobSpawnPoint;
    public float blobSpeed = 5f;
    public float blobArcHeight = 2f;
    public float blobSpacing = 0.5f;
    public float blobLifetime = 5f;
    public int numberOfBlobs = 4;
    public int blobDamage = 10;
    public float attackCd = 5;
    public float attackTimer = 0;
    public float attackWaitTime = 3;
    public float rageAttackWaitTime = 1.5f;
    public float rageAttackDuration = 4;
    public float RageAttackVelocity = 10;

    private GameObject player;
    private bool isAttacking = false;
    private bool isEnraged = false;
    private bool isRage = true;
    private Rigidbody2D slimeRB;
    private bool Colisionando = false;
    private Vector3 direccionRebote;
    private float fuerzaRebote;
    private float originalScale;
    private Vector2 direccionIncidencia;
    private AI_SlimeBoss boss;
    private bool Raging = false;
    private bool firstRage;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Jugador");
        slimeRB = GetComponent<Rigidbody2D>();
        originalScale = slimeRB.gravityScale;
        boss = GetComponent<AI_SlimeBoss>();
        firstRage = false;
    }

    private void Update()
    {
        
        direccionIncidencia = slimeRB.velocity;
        attackTimer += Time.deltaTime;
        if (attackTimer > attackCd && !isAttacking && !isEnraged)
        {
            StartCoroutine(PerformAttackCoroutine());
            attackTimer = 0f;
        }
        else if (attackTimer > attackCd && !isAttacking && isRage && isEnraged)
        {
            Debug.Log("BUG");
            while (!boss.getCanJump()){}
            StartCoroutine(PerformRageAttackCoroutine());
            attackTimer = 0f;
        }
        else if (attackTimer > attackCd && !isAttacking && !isRage && isEnraged)
        {
            StartCoroutine(PerformAttackCoroutine());
            attackTimer = 0f;
        }

        if (Raging){
            slimeRB.velocity = direccionRebote;
        }

    }

    private System.Collections.IEnumerator PerformAttackCoroutine()
    {
        isAttacking = true;
        PauseSlimeMovement();

        yield return new WaitForSeconds(attackWaitTime);

        Vector2 playerPosition = player.transform.position;

        // Calculate the angle between the slime and the player
        Vector2 direction = playerPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate the spacing between each blob
        float spacingAngle = 360f / numberOfBlobs;

        for (int i = 0; i < numberOfBlobs; i++)
        {
            // Calculate the angle for the current blob
            float currentAngle = angle + (i * spacingAngle);

            // Calculate the direction vector for the current blob
            Vector2 currentDirection = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.right;

            // Calculate the target position with the offset
            Vector2 targetPosition = playerPosition + (currentDirection.normalized * blobSpacing);

            // Instantiate the blob projectile
            GameObject blob = Instantiate(blobPrefab, blobSpawnPoint.position, Quaternion.identity);

            // Set the blob's initial speed and direction towards the target position
            Rigidbody2D rb = blob.GetComponent<Rigidbody2D>();
            rb.velocity = (targetPosition - (Vector2)blobSpawnPoint.position).normalized * blobSpeed;

            // Set the blob's arc trajectory
            Vector3 blobArc = new Vector3(currentDirection.x, currentDirection.y + blobArcHeight, 0f);
            rb.AddForce(blobArc, ForceMode2D.Impulse);

            // Set the blob's lifetime
            Destroy(blob, blobLifetime);
        }

        ResumeSlimeMovement();
        isAttacking = false;
        if (isEnraged)
        {
            isRage = true;
            Debug.Log("JAJANT");
        }
    }

    private IEnumerator PerformRageAttackCoroutine(){
        isAttacking = true;
        firstRage = true;
        PauseSlimeMovement();

        yield return new WaitForSeconds(rageAttackWaitTime);

        int sentido = Random.Range(0,2);
        float valor = Random.Range(0f,1f);

        if (sentido == 1)
        {
            slimeRB.AddForce(new Vector2(RageAttackVelocity * valor, RageAttackVelocity), ForceMode2D.Impulse);
        }
        else
        {
            slimeRB.AddForce(new Vector2(RageAttackVelocity * valor * -1, RageAttackVelocity), ForceMode2D.Impulse);
        }
        direccionRebote = new Vector2(RageAttackVelocity * valor, RageAttackVelocity);
        Debug.Log(slimeRB.velocity.x + " " + slimeRB.velocity.y);
        Raging = true;

        yield return new WaitForSeconds(rageAttackDuration);

        ResumeSlimeMovement();
        isAttacking = false;
        isRage = false;
        Raging = false;
    }

    private void PauseSlimeMovement()
    {
        gameObject.GetComponent<AI_SlimeBoss>().enabled = false;
    }

    private void ResumeSlimeMovement()
    {
        gameObject.GetComponent<AI_SlimeBoss>().enabled = true;
    }

    public void rageMode(){
        isEnraged = true;
    }

    void OnCollisionEnter2D(Collision2D colision){
        direccionRebote = Vector2.Reflect(direccionIncidencia, colision.contacts[0].normal);
        Colisionando = true;
    }

    void OnCollisionExit2D(Collision2D colision){
        Colisionando = false;
    }
}
