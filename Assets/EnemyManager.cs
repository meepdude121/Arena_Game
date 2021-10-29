using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{    
    public List<WaveObject> waveObjects = new List<WaveObject>();
    public int Wave = 0;
    public static EnemyManager instance;
    public int AliveEnemies = 0;
    public int MaxEnemies => Mathf.Clamp(Mathf.FloorToInt(LocalDifficulty), 1, int.MaxValue);
    public float GlobalDifficulty = 1f;
    public float LocalDifficulty = 0f;

    public Slider waveSlider;
    public TextMeshProUGUI waveText;
    private void Awake() {
        instance = this;
        // find all wave objects in the folder Resources/Waves
        waveObjects.AddRange(Resources.LoadAll<WaveObject>("Waves"));
        // sort them in order of difficulty
        waveObjects.Sort();
        foreach (WaveObject wave in waveObjects) Debug.Log($"{wave}({wave.difficultyThreshold})");

        OnChangeWave();
    }
    public void OnEnemyDeath() {
        // scale difficulty
        LocalDifficulty += ((GlobalDifficulty * 0.1f) + (LocalDifficulty * 0.1f)) * 0.5f;
        // get next wave
        Wave = GetWaveFromDifficulty();
        // decrement alive enemies counter, as this function runs on enemy death
        AliveEnemies -= 1;
        waveSlider.value = LocalDifficulty;
        if (waveObjects[Wave + 1] == null)
            waveText.text = $"Max wave! Score: {LocalDifficulty * 1000}";
    }
    private int GetWaveFromDifficulty() {
        // check whether the next wave exists, and if it does check whether LocalDifficulty meets the difficulty threshold
        if ((Wave + 1 < waveObjects.Count) && (LocalDifficulty > waveObjects[Wave + 1].difficultyThreshold)) {
            OnChangeWave();
            return Wave + 1;
        }

        // else do nothing
        return Wave;
    }
    public GameObject GetNewEnemy()
    {
        // if there are less alive enemies than the current maximum, get a random prefab from the wave data
        if (AliveEnemies < MaxEnemies) return waveObjects[Wave].enemyPrefabs[Random.Range(0, waveObjects[Wave].enemyPrefabs.Length)];

        // otherwise return null
        return null;
    }

    private void OnChangeWave() {
        // if on last wave
        if (waveObjects[Wave + 1] == null) {
            // Full slider
            waveSlider.minValue = waveSlider.maxValue = waveSlider.value = 1;
            waveText.text = $"Max wave! Score: {LocalDifficulty * 1000}";
            return;
        }
        // set gui object limits
        waveSlider.minValue = waveObjects[Wave].difficultyThreshold;
        waveSlider.maxValue = waveObjects[Wave + 1].difficultyThreshold;
        waveText.text = $"Wave {Wave + 1}";
    }
}
