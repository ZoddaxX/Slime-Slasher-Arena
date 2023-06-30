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
    public float slimeDeathVolume; 
    public AudioClip slimeDeathSound;

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
        while (audioSource.isPlaying){}
        audioSource.volume = aux;
    }
}
