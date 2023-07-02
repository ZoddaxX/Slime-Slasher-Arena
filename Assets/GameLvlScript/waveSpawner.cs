using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting}
    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;

    public float spawnRange = 40f;

    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float searchCountDown = 1f;

    public SpawnState state = SpawnState.counting;

    public GameObject stats;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }
    private void Update()
    {
        if (state == SpawnState.waiting)
        {
            if (!EnemyStillAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if(waveCountDown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                //codigo pa spawnear
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    bool EnemyStillAlive()
    {
        searchCountDown -= Time.deltaTime;

        if (searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemigo") == null && GameObject.FindGameObjectWithTag("Jefe") == null)
            {
                return false;
            }
        }
        

        return true;
    }

    void WaveCompleted()
    {
        state = SpawnState.counting;
        waveCountDown = timeBetweenWaves + 7;
        nextWave++;
        if(nextWave + 1 > waves.Length)
        {
            nextWave = 0;
            Debug.Log("Oleadas completadas");
            ControladorSonido.Instance.StopMainMusic();
            StartCoroutine(HabilityPoints());
        }
        else if (nextWave == 2){
            float valor = 0;
            while (valor < 3f){
                valor += Time.deltaTime;
            }
            ControladorSonido.Instance.PlayBossTheme();
        }
    }
    private IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        state = SpawnState.waiting;

        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        float randomNumber = Random.Range(-spawnRange, spawnRange);
        Vector3 spawntransform = transform.position;
        spawntransform.x += randomNumber;
        spawntransform.y -= 5;
        Instantiate(_enemy, spawntransform, transform.rotation);
        Debug.Log("spawning enemy");

    }

    public int getWave(){
        if (nextWave == 0) return 3;
        return nextWave - 1;
    }
    IEnumerator HabilityPoints(){
        yield return new WaitForSeconds(2);
        ControladorSonido.Instance.PlayVictorySound();
        yield return new WaitForSeconds(3);
        stats.SetActive(true);
        NewStats statsScript = stats.GetComponent<NewStats>();
        statsScript.RestartScript(); 
    }
}
