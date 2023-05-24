using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Stats : MonoBehaviour
{
  [SerializeField] private float health;
  
  public void TomarDaño(float damage)
  {
    health -= damage;

    if (health <= 0)
    {
      Death();
    } else{
      Debug.Log("AAAAAAAAAAAAAA");
    }
  }

  private void Death()
  {
    Debug.Log("Game Over.");
  }
}
