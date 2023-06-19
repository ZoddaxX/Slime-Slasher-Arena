using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject objetoPrefab;
    public float minPositionX;
    public float maxPositionX;
    public float tiempoEspera;
    public int enemyAmount;
    public bool isRandomGroup;
    public int minEnemyAmount;
    public int maxEnemyAmount; 

    private void Start()
    {
        GenerarObjeto();
        InvokeRepeating("GenerarObjeto", tiempoEspera, tiempoEspera);
    }

    private void GenerarObjeto()
    {
        int cantidadEnemigos;

        if (isRandomGroup)
        {
            cantidadEnemigos = Random.Range(minEnemyAmount, maxEnemyAmount + 1);
        }
        else
        {
            cantidadEnemigos = enemyAmount;
        }

        for (int count = 0; count < cantidadEnemigos; count++)
        {
            float positionX = Random.Range(minPositionX + transform.position.x, maxPositionX + transform.position.x);
            Vector2 position = new Vector2(positionX, transform.position.y);
            Instantiate(objetoPrefab, position, Quaternion.identity);
        }
    }
}
