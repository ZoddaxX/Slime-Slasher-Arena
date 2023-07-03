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
    public int puntosHabilidadInicial = 2;
    public int puntosHabilidad;
    public TextMeshProUGUI Puntos;
    public TextMeshProUGUI LAttack;
    public TextMeshProUGUI HAttack;
    public TextMeshProUGUI Health;
    public AudioClip HabilitySelectionTheme;
    public float MeleeSlimeDamageIncrease;
    public float MeleeSlimeHealthIncrease;
    public float BossSlimeMeleeDamageIncrease;
    public float BossSlimeRangedDamageIncrease;
    public float BossSlimeHealthIncrease; 
    public static float MeleeSlimeDamage;
    public static float MeleeSlimeHealth;
    public static float BossSlimeMeleeDamage;
    public static float BossSlimeRangedDamage;
    public static float BossSlimeHealth;

    private AudioSource audioSource;

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
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;
        audioSource.clip = HabilitySelectionTheme;
        audioSource.Play();
        Time.timeScale = 0f;
    }

    public void BuffEnemies(){
        MeleeSlimeDamage += MeleeSlimeDamageIncrease;
        MeleeSlimeHealth += MeleeSlimeHealthIncrease;
        BossSlimeMeleeDamage += BossSlimeMeleeDamageIncrease;
        BossSlimeRangedDamage += BossSlimeRangedDamageIncrease;
        BossSlimeHealth += BossSlimeHealthIncrease;
    }

    void Update(){
        if (puntosHabilidad == 0)
        {
            puntosHabilidad = puntosHabilidadInicial;
            BuffEnemies();
            Time.timeScale = 1f;
            ControladorSonido.Instance.PlayBattleTheme();
            audioSource.Stop();
            Puntos.text = puntosHabilidad.ToString();
            gameObject.SetActive(false);

        }
    }
}
