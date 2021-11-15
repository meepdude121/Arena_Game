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
    public int MaxEnemies => waveObjects[Wave].maxEnemies;
    public float GlobalDifficulty = 1f;
    public float LocalDifficulty = 0f;
    Coroutine smoothToPointInstance;
    public Slider waveSlider;
    public TextMeshProUGUI waveText;
    public bool CanSpawn;
    private void Awake() {
        // get global instance to this component
        instance = this;
        // find all wave objects in the folder Resources/Waves
        waveObjects.AddRange(Resources.LoadAll<WaveObject>("Waves"));
        // sort them in order of difficulty
        waveObjects.Sort();

        OnChangeWave();
    }
    public void OnEnemyDeath(Entity entity) {
        // scale difficulty
        LocalDifficulty += entity.difficultyWeight * GlobalDifficulty * 0.3f;
        // get next wave
        GetWaveFromDifficulty();
        // decrement alive enemies counter, as this function runs on enemy death
        AliveEnemies -= 1;
        if (smoothToPointInstance != null)
            StopCoroutine(smoothToPointInstance);
        smoothToPointInstance = StartCoroutine(SmoothToPoint());
        
        if (waveObjects.Count - 1 < Wave + 1)
            waveText.text = $"Max wave! Score: {(int)(LocalDifficulty * 1000)}";
    }
    private void GetWaveFromDifficulty() {
        // check whether the next wave exists, and if it does check whether LocalDifficulty meets the difficulty threshold
        if ((Wave + 1 < waveObjects.Count) && (LocalDifficulty > waveObjects[Wave + 1].difficultyThreshold)) {
            // increment wave
            Wave++;
            OnChangeWave();
        }
    }
    public GameObject GetNewEnemy()
    {
        // if spawning is enabled (not in wave cooldown)
        if (CanSpawn) {
            // if there are less alive enemies than the current maximum, get a random prefab from the wave data
            if (AliveEnemies < MaxEnemies) return waveObjects[Wave].enemyPrefabs[Random.Range(0, waveObjects[Wave].enemyPrefabs.Length)];
        }
        // otherwise return null
        return null;
    }

    private void OnChangeWave() {
        // if on last wave
        if (waveObjects.Count - 1 < Wave + 1) {
            // Full slider
            waveSlider.minValue = 0;
            waveSlider.maxValue = waveSlider.value = 1;
            waveText.text = $"Max wave! Score: {(int)(LocalDifficulty * 1000)}";
            return;
        } else {
        // set gui object limits
        waveSlider.minValue = Mathf.Clamp(waveObjects[Wave].difficultyThreshold, 1, float.PositiveInfinity);
        waveSlider.maxValue = waveObjects[Wave + 1].difficultyThreshold;
        waveText.text = $"Wave {Wave + 1}";

        // start wave timer
        StartCoroutine(WaveTimer(5f));
        }
    }

    private IEnumerator WaveTimer(float time) { 
        LocalDifficulty = Mathf.Clamp(waveObjects[Wave].difficultyThreshold, 1f, float.PositiveInfinity);
        // disallow spawning
        CanSpawn = false;

        // loop while time > 0
        while (time > 0) {
            // remove the last frame time from T
            time -= Time.deltaTime;
            // set waveText to formatted wave
            waveText.text = $"Wave {Wave + 1}\n <size=44px>Starts in {Mathf.RoundToInt(time)}</size>";
            // stop processing until next wave
            yield return null;
        } 
        // allow spawning
        CanSpawn = true;
        waveText.text = $"Wave {Wave + 1}";
        LocalDifficulty = Mathf.Clamp(waveObjects[Wave].difficultyThreshold, 1f, float.PositiveInfinity);
    }

    private IEnumerator SmoothToPoint() {
        float t = 0f;
        float multiplier = 3f;
        while (t < multiplier) {
            waveSlider.value = Mathf.Lerp(waveSlider.value, LocalDifficulty, t / multiplier);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
