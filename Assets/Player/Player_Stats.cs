using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Stats : MonoBehaviour
{
  public Slider slider;
  public bool alive = true;
  public GameObject attackCenter;
  public GameObject gameOverPanel;

  [SerializeField] private float maxHp;
  [SerializeField] private float health;
  [SerializeField] private float DmgCooldownTime;
  [SerializeField] private float DmgCooldown;

  public void Start()
  {
    health = maxHp;
  }

  public void Update(){
    if (DmgCooldown > 0) DmgCooldown -= Time.deltaTime;
  }
  public void TomarDano(float damage)
  {
    if (DmgCooldown <= 0){
      health -= damage;
      slider.transform.GetComponent<HpBarScript>().setHealth(health,maxHp);
      if (health <= 0) Death(alive);
      DmgCooldown = DmgCooldownTime;
    }
  }

  private void Death(bool alive)
  {
    alive = false;
    attackCenter.GetComponent<PlayerAttack>().enabled = false;
    gameObject.GetComponent<PlayerController>().enabled = false;
    gameOverPanel.SetActive(true);
    Debug.Log("Game Over.");
  }

  public void AddHealth(){
    health += 5;
    maxHp += 5;
    slider.transform.GetComponent<HpBarScript>().setHealth(health,maxHp);
  }

  public float GetMaxHealth(){
    return maxHp;
  }

  public void moreHealth(float vida){
    if (health + vida > maxHp) health = maxHp;
    else health += vida;
    slider.transform.GetComponent<HpBarScript>().setHealth(health,maxHp);
  }
}
