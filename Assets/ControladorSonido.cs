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
        if (!audioSource1.isPlaying) audioSource1.PlayOneShot(sonido);
        else if (!audioSource2.isPlaying) audioSource2.PlayOneShot(sonido);
        else if (!audioSource3.isPlaying) audioSource3.PlayOneShot(sonido);
        else if (!audioSource4.isPlaying) audioSource4.PlayOneShot(sonido);
        else audioSource5.PlayOneShot(sonido);
    }
}
