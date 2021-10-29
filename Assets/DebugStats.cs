using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStats : MonoBehaviour
{
    List<TMPro.TextMeshProUGUI> TextComponents = new List<TMPro.TextMeshProUGUI>();
    private void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            TextComponents.Add(transform.GetChild(i).GetComponent<TMPro.TextMeshProUGUI>());
        }
    }

    private void Update() {
        TextComponents[0].text = $"Wave {EnemyManager.instance.Wave}";
        TextComponents[1].text = $"LocalDifficulty: {EnemyManager.instance.LocalDifficulty}";
        TextComponents[2].text = $"Enemies: {EnemyManager.instance.AliveEnemies}/{EnemyManager.instance.MaxEnemies}";
    }
}
