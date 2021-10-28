using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int AliveEnemies = 0;
    public int MaxEnemies => Mathf.FloorToInt(LocalDifficulty);
    public float GlobalDifficulty = 1f;
    public float LocalDifficulty = 0f;
    public void OnEnemyDeath() {
        LocalDifficulty = LocalDifficulty + (GlobalDifficulty + (LocalDifficulty * 0.1f)) + 0.1f;
        AliveEnemies -= 1;
    }
}
