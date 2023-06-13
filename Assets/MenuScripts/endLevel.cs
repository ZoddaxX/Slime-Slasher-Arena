using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    public GameObject menuUI; // Reference to the menu UI object
    public GameObject enemyToWatch; // Reference to the enemy to watch for death
    public bool spawned = false;

    private void Update()
    {
        
        // Subscribe to the death event of the enemy
        if (enemyToWatch != null)
        {
            spawned = true;
        }
        else if (spawned)
        {
            OnEnemyDeath();
        }
    }

    private void OnEnemyDeath()
    {
        // Enable the menu UI when the specific enemy dies
        menuUI.SetActive(true);
        PlayerPrefs.SetInt("TutorialCompleted", 1);
    }
}