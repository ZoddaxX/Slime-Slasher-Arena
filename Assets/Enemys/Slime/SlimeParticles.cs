using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeParticles : MonoBehaviour
{
    public GameObject player;
    public float rotationModifier = 90;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Vector3 vectortotrget = transform.position - player.transform.position;
        float angle = Mathf.Atan2(vectortotrget.y, vectortotrget.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.down);
        transform.rotation =  q;

    }
}   
