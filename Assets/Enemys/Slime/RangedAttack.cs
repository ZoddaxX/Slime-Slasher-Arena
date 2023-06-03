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
    public int blobDamage = 5;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PerformRangedAttack()
    {
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

            // Instantiate the blob projectile
            GameObject blob = Instantiate(blobPrefab, blobSpawnPoint.position, Quaternion.identity);

            // Set the blob's initial speed and direction
            Rigidbody2D rb = blob.GetComponent<Rigidbody2D>();
            rb.velocity = currentDirection.normalized * blobSpeed;

            // Set the blob's arc trajectory
            Vector3 blobArc = new Vector3(currentDirection.x, currentDirection.y + blobArcHeight, 0f);
            rb.AddForce(blobArc, ForceMode2D.Impulse);

            // Set the blob's lifetime
            Destroy(blob, blobLifetime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player if hit by a blob
            Player_Stats playerStats = collision.gameObject.GetComponent<Player_Stats>();
            if (playerStats != null)
            {
                playerStats.TomarDaño(blobDamage);
            }
        }

        // Destroy the projectile on collision with a surface or the player
        Destroy(gameObject);
    }
}