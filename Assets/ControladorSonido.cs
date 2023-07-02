using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    public static ControladorSonido Instance;
    private AudioSource audioSource1;
    private AudioSource audioSource2;
    private AudioSource audioSource3;
    private AudioSource audioSource4;
    private AudioSource audioSource5;
    private AudioSource audioSource6;
    public float slimeDeathVolume; 
    public AudioClip slimeDeathSound;
    public AudioClip BattleTheme;
    public AudioClip BossTheme;
    public AudioClip VictoryTheme1;
    public AudioClip VictoryTheme2;
    public waveSpawner wave;

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource1 =  GetComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource3 = gameObject.AddComponent<AudioSource>();
        audioSource4 = gameObject.AddComponent<AudioSource>();
        audioSource5 = gameObject.AddComponent<AudioSource>();
        audioSource6 = gameObject.AddComponent<AudioSource>();

        audioSource6.clip = BattleTheme;
        audioSource6.volume = 0.3f;
        audioSource6.Play();
    }

    public void ReproducirSonido(AudioClip sonido){
        if (!audioSource1.isPlaying) VerificarSonido(sonido, audioSource1);
        else if (!audioSource2.isPlaying) VerificarSonido(sonido, audioSource2);
        else if (!audioSource3.isPlaying) VerificarSonido(sonido, audioSource3);
        else if (!audioSource4.isPlaying) VerificarSonido(sonido, audioSource4);
        else VerificarSonido(sonido, audioSource5);
    }

    private void VerificarSonido(AudioClip sonido, AudioSource audioSource){
        float aux = audioSource.volume;
        if (sonido == slimeDeathSound)
        {
            audioSource.volume = slimeDeathVolume;
        }
        audioSource.PlayOneShot(sonido);
        audioSource.volume = aux;
    }

    public void PlayBattleTheme(){
        audioSource6.clip = BattleTheme;
        audioSource6.Play();
    }

    public void PlayBossTheme(){
        audioSource6.clip = BossTheme;
        audioSource6.Play();
    }

    public void PlayVictorySound(){
        int valor =  Random.Range(1,3);
        if (valor == 1)
        {
            audioSource6.clip = VictoryTheme1;
            audioSource6.Play();
        }
        else
        {
            audioSource6.clip = VictoryTheme2;
            audioSource6.Play();
        }
    }

    public void StopMainMusic(){
        audioSource6.Stop();
    }
}
