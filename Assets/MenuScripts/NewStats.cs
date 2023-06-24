using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewStats : MonoBehaviour
{
    public GameObject player;
    public PlayerAttack playerattack;
    public Player_Stats playerstats;
    public int puntosHabilidadInicial;
    public int puntosHabilidad;
    public TextMeshProUGUI Puntos;
    public TextMeshProUGUI LAttack;
    public TextMeshProUGUI HAttack;
    public TextMeshProUGUI Health;

    void Start(){
        player = GameObject.Find("Player");
        puntosHabilidad = puntosHabilidadInicial;
        playerattack = FindObjectOfType<PlayerAttack>();
        playerstats = FindObjectOfType<Player_Stats>();
        Puntos.text = puntosHabilidad.ToString();
        Time.timeScale = 0f;
        LAttack.text = playerattack.GetLightAttack().ToString();
        HAttack.text = playerattack.GetHeavyAttack().ToString();
        Health.text = playerstats.GetMaxHealth().ToString();
    }

    public void moreLightAttack(){
        playerattack.AddLightAttack();
        LAttack.text = playerattack.GetLightAttack().ToString();
        puntosHabilidad--;
        Puntos.text = puntosHabilidad.ToString();
    }

    public void moreHeavyAttack(){
        playerattack.AddHeavyAttack();
        HAttack.text = playerattack.GetHeavyAttack().ToString();
        puntosHabilidad--;
        Puntos.text = puntosHabilidad.ToString();
    }

    public void moreHealth(){
        playerstats.AddHealth();
        Health.text = playerstats.GetMaxHealth().ToString();
        puntosHabilidad--;
        Puntos.text = puntosHabilidad.ToString();
    }

    public void RestartScript(){
        Time.timeScale = 0f;
    }

    void Update(){
        if (puntosHabilidad == 0)
        {
            puntosHabilidad = puntosHabilidadInicial;
            Time.timeScale = 1f;
            gameObject.SetActive(false);

        }
    }
}
