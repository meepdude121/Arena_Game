using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    List<GameObject> enemyPrefabs = new List<GameObject>();
    List<WaveObject> waveObjects = new List<WaveObject>();
    Dictionary<float, WaveObject> waves = new Dictionary<float, WaveObject>();
    EnemyManager enemyManager;
    private void Awake() {
        enemyPrefabs.AddRange(Resources.LoadAll<GameObject>("Enemies"));
        waveObjects.AddRange(Resources.LoadAll<WaveObject>("Waves"));

        foreach(WaveObject wave in waveObjects){
            waves.Add(wave.difficultyThreshold, wave);
        }

        enemyManager = EnemyManager.instance;
    }
    private void Update() {
        StartCoroutine(loopOnSecond());
    }

    IEnumerator loopOnSecond() {
        WaveObject currentWave = waves[0];
        int currentWaveIndex = 0;
        while (true){
            if (enemyManager.AliveEnemies > enemyManager.MaxEnemies){
                if (enemyManager.LocalDifficulty > waves[currentWaveIndex + 1].difficultyThreshold) {
                    currentWaveIndex++;
                    currentWave = waves[currentWaveIndex];
                }
            }

            int randomFromRange = Random.Range(0, currentWave.enemyPrefabs.Length);
            var instantiatedEnemy = Instantiate(enemyPrefabs[randomFromRange]);
            instantiatedEnemy.transform.position = transform.position;

            yield return new WaitForSeconds(1);
        }
        
    }
}
