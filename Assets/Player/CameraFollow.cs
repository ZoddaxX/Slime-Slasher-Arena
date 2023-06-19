using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speedFollow = 2f;
    public Transform player;

    [SerializeField] private float offset = 5;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y + offset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, speedFollow * Time.deltaTime);
    }
}
