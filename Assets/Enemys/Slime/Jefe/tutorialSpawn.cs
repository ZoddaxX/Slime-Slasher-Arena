using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialSpawn : MonoBehaviour
{
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    private bool hasSpawnedBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador") && !hasSpawnedBoss)
        {
            Debug.Log("Spawning Boss");
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        hasSpawnedBoss = true;
        Destroy(gameObject);
    }
}