using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab, triggerPoint;

    public bool isSpawning = false;

    public List<Rigidbody2D> enemyPool = new List<Rigidbody2D>();
    public int poolSize = 10;

    public Transform spawnPoint, stopPoint;
    float distance;

    // Start is called before the first frame update
    void Start()
    {   
            InitObjectPool();  
    }

    public void startSpawning()
    {
        print("Starting Corutine");
        isSpawning = true;
        StartCoroutine(spawnEnemy());

    }
    public void stopSpawning()
    {
        print("STOPPING COrutine");
        isSpawning = false;
        StopCoroutine(spawnEnemy());
    }
   

    void InitObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);

            Rigidbody2D enemRB = enemy.GetComponent<Rigidbody2D>();
            enemyPool.Add(enemRB);
        }
    }

    IEnumerator spawnEnemy()
    {
        while (isSpawning)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnPoint.transform.position;

            yield return new WaitForSeconds(2);

        }


    }

    


}
