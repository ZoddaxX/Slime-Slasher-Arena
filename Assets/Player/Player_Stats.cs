using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
  public Slider slider;
  public bool alive = true;
  public GameObject gameOverPanel;

  [SerializeField] private float maxHp;
  [SerializeField] private float health;

  public void Start()
  {
    health = maxHp;
  }
  public void TomarDano(float damage)
  {
    health -= damage;
    slider.transform.GetComponent<HpBarScript>().setHealth(health,maxHp);
    if (health <= 0) Death(alive);
  }

  private void Death(bool alive)
  {
    alive = false;
    gameObject.GetComponent<PlayerAttack>().enabled = false;
    gameObject.GetComponent<PlayerController>().enabled = false;
    gameOverPanel.SetActive(true);
    Debug.Log("Game Over.");
  }
}
