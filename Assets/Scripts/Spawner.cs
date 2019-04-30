using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform enemyPref;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;

    private int waveIndex = 0;

    private float countdown = 2f;

    public Text cdIndex;

    private int _enemyCount;

    private List<Wave> waves;

    void Start()
    {
        waves = new List<Wave>();
        waves.Add(new Wave(15, 1));
        waves.Add(new Wave(13, 2));
        waves.Add(new Wave(20, 2));
        waves.Add(new Wave(20, 3));
        waves.Add(new Wave(25, 3));
        waves.Add(new Wave(25, 5));
        waves.Add(new Wave(35, 5));
        waves.Add(new Wave(40, 6));
        waves.Add(new Wave(40, 8));
        waves.Add(new Wave(45, 8));
        waves.Add(new Wave(400, 1));
        
    }

    public int GetWaveIndex()
    {
        return waveIndex;
    }

    void Update()
    {
        if (waveIndex == waves.Count)
        {
            cdIndex.text = "BOSS";

            //this.enabled = false;
            return;
        } 
        if ( countdown <= 0f && waveIndex < waves.Count)
        {
            StartCoroutine(Spawn());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        countdown = countdown < 0 ? 0 : countdown;
        cdIndex.text = "New wave in : " +  Math.Round(countdown, 2, MidpointRounding.AwayFromZero).ToString() + (Math.Round(countdown, 2, MidpointRounding.AwayFromZero).ToString().Length > 3 ? "" : "0");
        
    }

    IEnumerator Spawn()
    {

        for (int i = 0; i < waves[waveIndex].GetEnemyCount(); i++)
        {
            SpawnEnemy(waves[waveIndex].GetEnemyHealth());
            yield return new WaitForSeconds(0.5f);
        }
        waveIndex++;
    }
    void SpawnEnemy(float health)
    {
        var enemy = Instantiate(enemyPref, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<Enemy>().SetHealth(health);
        if (waveIndex == waves.Count - 1) enemy.GetComponent<Enemy>().IsBoss = true;
    }
}
