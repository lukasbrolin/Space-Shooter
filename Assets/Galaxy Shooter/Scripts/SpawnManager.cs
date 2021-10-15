using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField] 
    private GameObject[] _powerUp;
    [SerializeField] 
    private GameObject _powerUpContainer;

    private bool _gameRunning = true;

    void Start()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_gameRunning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-7.8f, 7.8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemy, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(0.7f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_gameRunning)
        {
            int randomPowerUp = Random.Range(0, 3);
            Vector3 posToSpawn = new Vector3(Random.Range(-7.8f, 7.8f), 7, 0);
            GameObject newPowerUp = Instantiate(_powerUp[randomPowerUp], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }


    public void StopRunning()
    {
        _gameRunning = false;
    }
}
