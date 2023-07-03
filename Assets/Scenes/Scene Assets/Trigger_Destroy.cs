using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Destroy : MonoBehaviour
{
    public GameObject enemigo1;
    public GameObject enemigo2;
    public GameObject enemigo3;
    public GameObject enemigo4;

    private GameObject entity1;
    private GameObject entity2;
    private GameObject entity3;
    private GameObject entity4;


    // Start is called before the first frame update
    void Start()
    {
        entity1 = GameObject.Find(enemigo1.name);
        entity2 = GameObject.Find(enemigo2.name);
        entity3 = GameObject.Find(enemigo4.name);
        entity4 = GameObject.Find(enemigo4.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (entity1 == null && entity2 == null && entity3 == null && entity4 == null ) DestroyImmediate(gameObject);
    }
}
