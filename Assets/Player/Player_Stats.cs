using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
  [SerializeField] private float health;
  public Slider slider;
  //public HealthBar healthBar;
  
  public void TomarDaño(float damage)
  {
    health -= damage;
    //healthBar.setHealth(health, 100);

    if (health > 0) Death();
  }

  private void Death()
  {
    Debug.Log("Game Over.");
  }
}
