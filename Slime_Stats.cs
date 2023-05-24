using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_Stats : MonoBehaviour
{
  [SerializeField] private float health;
  
  public void TomarDaño(float damage)
  {
    health -= damage;

    if (health <= 0)
    {
      Death();
    } else{
      Debug.Log("Auchis");
    }
  }

  private void Death()
  {
    Debug.Log("Me morí x.x");
  }
}
