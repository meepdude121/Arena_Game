using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WaveObject", order = 1)]
public class WaveObject : ScriptableObject, System.IComparable
{
    public float difficultyThreshold;
    public GameObject[] enemyPrefabs;
    public int maxEnemies;
    public float damageMultiplier;
    public float healthMultiplier;
    public int CompareTo(object obj) {
        if (obj == null) return 1;

        WaveObject otherWave = obj as WaveObject;
        if (otherWave != null)
            return this.difficultyThreshold.CompareTo(otherWave.difficultyThreshold);
        else
           throw new System.ArgumentException("Object compared is not a WaveObject");
    }
}
