using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    public static ControladorSonido Instance;
    private AudioSource audioSource; 

    void Awake(){
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource =  GetComponent<AudioSource>();
    }

    public void ReproducirSonido(AudioClip sonido){
        audioSource.PlayOneShot(sonido);
    }
}
