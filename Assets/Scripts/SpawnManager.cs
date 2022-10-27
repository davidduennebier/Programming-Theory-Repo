using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyToSpawn;
    [SerializeField] private float enemySpawnPointZ = 20;
    [SerializeField] private int wave = 1;
    [SerializeField] private float singleUnitSpawnTime = 0.5f;
    private float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: die unschönste Lösung. Muss später noch umgewandelt werden.
        InvokeRepeating("SpawnEnemyWave", 1.0f, 20.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemyWave()
    {
        // hier ließen sich die Waves jetzt designen über switch cases
        for (int i = 0; i < wave; i++)
        {
            t += singleUnitSpawnTime;
            StartCoroutine("SpawnNext");
        }
        t = 0;
        wave++;
    }
    private IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(t);
        Instantiate(EnemyToSpawn[1], new Vector3(0, EnemyToSpawn[0].transform.position.y, enemySpawnPointZ), EnemyToSpawn[0].transform.rotation);
    }
}
