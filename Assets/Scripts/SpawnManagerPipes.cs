using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerPipes : MonoBehaviour
{
    [SerializeField]
    private GameObject _pipesContainer;
    [SerializeField]
    private GameObject[] _pipes;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
    }
    IEnumerator EnemySpawnRoutine()
    {
        while (!_stopSpawning)
        {
            int randomEnemy = Random.Range(0, 2); //This is to have an array of different pipes if I wanna do different assets or styles, change the value if you add more to the array
            float randomY = Random.Range(-3.33f, 0.3f); //This will randomize where it can spawn a pipe in Y 
            if (randomEnemy == 1)
            {
                randomY = Random.Range(-3.33f, 1.15f); //This will randomize where it can spawn a pipe in Y when it's tile 0
            }
            else if(randomEnemy == 0)
            {
                randomY = Random.Range(-3.7f, 1.7f); //This will randomize where it can spawn a pipe in Y when it's tile 0
            }
            else
            {
                Debug.LogError("Didnt work");
            }
            
            Vector3 direction = new Vector3(9, randomY, 0);
            Instantiate(_pipes[randomEnemy], direction, Quaternion.identity);
            yield return new WaitForSeconds(4f);
        }
    }
    public void OnPlayerStart()
    {
        StartCoroutine(EnemySpawnRoutine());
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
