using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Spawner : MonoBehaviour
{
    public GameObject Spawner;
    void OnTriggerEnter2D(){
        Spawner.SetActive(true);
        DestroyImmediate(gameObject);
    }
}
