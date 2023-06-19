using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Destroy : MonoBehaviour
{
    public GameObject enemigo; 
    private GameObject entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = GameObject.Find(enemigo.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (entity == null) DestroyImmediate(gameObject);
    }
}
