using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    /* Temp version of SpawnManager*/

    public GameObject player;
    public GameObject poolManagerObject;
    public Vector2 spawnRange;

    private PoolManager poolManager;

    // Start is called before the first frame update
    private void Start()
    {
        poolManager = poolManagerObject.GetComponent<PoolManager>();
    }

    // Update is called once per frame
    public void Spawn(PoolType index)
    {
        float x = Random.Range(-spawnRange.x, spawnRange.x);
        float z = Random.Range(-spawnRange.y, spawnRange.y);

        Vector3 spawnPosition = player.transform.position + new Vector3(x, 0, z);
        GameObject virus = poolManager.Get(index, spawnPosition, Quaternion.LookRotation(player.transform.position - spawnPosition));

        virus.GetComponent<VirusBehaviour>().Initialize(player, poolManager);
    }
}