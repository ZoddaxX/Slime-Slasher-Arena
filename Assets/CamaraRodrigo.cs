using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRodrigo : MonoBehaviour
{
    public float speedFollow = 2f;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, speedFollow * Time.deltaTime);
    }
}
