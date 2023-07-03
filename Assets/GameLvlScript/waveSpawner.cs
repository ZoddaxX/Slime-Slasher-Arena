using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class waveSpawner : MonoBehaviour
{
    public enum SpawnState { spawning, waiting, counting }

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
    public float timeBetweenWaves = 5f;
    public float waveCountDown;
    public SpawnState state = SpawnState.counting;
    public GameObject stats;
    public TextMeshProUGUI waveCounterText;
    public TextMeshProUGUI waveNameText;

    private int nextWave = 0;
    private float searchCountDown = 1f;

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

        if (waveCountDown <= 0)
        {
            if (waveCountDown <= 0 && state != SpawnState.spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
            
        }
        UpdateUI();
        
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
        waveCountDown = timeBetweenWaves;
        nextWave++;
        if (nextWave >= waves.Length)
        {
            nextWave = 0;
            Debug.Log("Oleadas completadas");
            ControladorSonido.Instance.StopMainMusic();
            StartCoroutine(HabilityPoints());
        }
        else if (nextWave == 2)
        {
            float valor = 0;
            while (valor < 3f)
            {
                valor += Time.deltaTime;
            }
            ControladorSonido.Instance.PlayBossTheme();
        }

        UpdateUI();
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

    public int getWave()
    {
        if (nextWave == 0) return 3;
        return nextWave - 1;
    }

    IEnumerator HabilityPoints()
    {
        yield return new WaitForSeconds(2);
        ControladorSonido.Instance.PlayVictorySound();
        yield return new WaitForSeconds(3);
        stats.SetActive(true);
        NewStats statsScript = stats.GetComponent<NewStats>();
        statsScript.RestartScript();

        // Update the UI
        waveCounterText.gameObject.SetActive(false);
        waveNameText.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        waveCounterText.gameObject.SetActive(state == SpawnState.counting);
        waveNameText.gameObject.SetActive(state == SpawnState.counting);

        if (state == SpawnState.counting)
        {
            waveCounterText.text = "Oleada " + (nextWave + 1).ToString() + " en ";
            waveNameText.text =  Mathf.Ceil(waveCountDown).ToString();
        }
        else
        {
            waveCounterText.text = "";
            waveNameText.text = "";
        }
    }
}
