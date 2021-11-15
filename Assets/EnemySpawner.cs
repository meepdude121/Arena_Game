using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnemyManager enemyManager;
    private void Awake() {
        enemyManager = EnemyManager.instance;
    }
    private void Start() {
        StartCoroutine(loopOnSecond());
    }

    public IEnumerator loopOnSecond() {
        // loop forever
        while (true) {
            var Enemy = enemyManager.GetNewEnemy();

            // if reference to enemy exists
            if (Enemy) {
                // create a new one
                Enemy = Instantiate(Enemy);
                // teleport it to the spawner
                Enemy.transform.position = transform.position;

                Entity EnemyEntityComponent = Enemy.GetComponent<Entity>();

                // scale health
                EnemyEntityComponent.Energy = EnemyEntityComponent.maxEnergy *= enemyManager.waveObjects[enemyManager.Wave].healthMultiplier;

                // scale gun damage
                Enemy.transform.GetChild(0).GetChild(0).GetComponent<Weapon>().BaseDamage *= enemyManager.waveObjects[enemyManager.Wave].damageMultiplier;

                // increment alive enemies counter
                enemyManager.AliveEnemies++;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
