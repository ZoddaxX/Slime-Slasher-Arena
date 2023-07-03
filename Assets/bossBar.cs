using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossBar : MonoBehaviour
{
    public Slider healthBar;
    public GameObject healthBarObject;
    public float searchInterval = 1f;
    public float healthMultiplier = 100f;

    private GameObject bossObject;
    private Boss_Stats bossStats;
    private bool isSearching;

    private void Start()
    {
        // Set the max value of the health bar
        healthBar.maxValue = 100f;

        // Start searching for the boss periodically
        InvokeRepeating(nameof(SearchForBoss), 0f, searchInterval);
    }

    private void Update()
    {
        if (bossObject != null && bossStats != null)
        {
            // Update the health bar value based on the boss's health percentage multiplied by the healthMultiplier
            healthBar.value = bossStats.healthPercentage * healthMultiplier;
        }
        else
        {
            // Deactivate the health bar if the boss is null
            healthBarObject.SetActive(false);
        }
    }

    private void SearchForBoss()
    {
        if (!isSearching)
        {
            // Search for the boss object with the "Jefe" tag
            bossObject = GameObject.FindGameObjectWithTag("Jefe");

            if (bossObject != null)
            {
                // Get the Boss_Stats component from the boss object
                bossStats = bossObject.GetComponent<Boss_Stats>();

                // Set the max value of the health bar to 100 since healthPercentage is already a percentage
                healthBar.maxValue = 100f;

                // Activate the health bar object
                healthBarObject.SetActive(true);
            }

            isSearching = false;
        }
    }
}