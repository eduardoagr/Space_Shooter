using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager: MonoBehaviour {

    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private List<GameObject> _powerUps;
    [SerializeField]
    private GameObject _powerUpsContainer = null;
    private bool _stopSpawning = false;

    internal void SpawnRoutines() {
        StartCoroutine(SpawnEnemy());
        StartCoroutine(PowerUpSpawn());
    }

    IEnumerator SpawnEnemy() {

        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false) {
            GameObject newEnemy = Instantiate(_enemy, new Vector3(Random.Range(-8.90f, 8.90f), 8f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator PowerUpSpawn() {

        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false) {
            int randomPowerUp = Random.Range(0, 2);
            GameObject newEnemy = Instantiate(_powerUps[randomPowerUp], new Vector3(Random.Range(-8.90f, 8.90f), 8f, 0f), Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(5.0f);
    }


    internal void isPlayerDead(bool dead) {

        if (dead) {

            _stopSpawning = dead;   
        }
    }
}