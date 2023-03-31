using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    public GameObject[] spawners;
    public List<Vector3> spawnersPositions;
    public float timeToSpawn = 5f;
    public float currentTimeToSpawn = 1f;

    private GameObject zombie;

    private void Start()
    {
        spawnersPositions = new List<Vector3>();
        spawners = GameObject.FindGameObjectsWithTag("Lane");
        foreach (var spawner in spawners) spawnersPositions.Add(spawner.transform.position);
    }

    private void Update()
    {
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeToSpawn = timeToSpawn;
        }
    }

    private void SpawnObject()
    {
        var lane = Random.Range(0, spawners.Length);
        zombie = ObjectPooler.SharedInstance.GetPooledObject();
        if (zombie != null)
        {
            zombie.transform.position = spawnersPositions[lane];
            zombie.SetActive(true);
        }
    }
}