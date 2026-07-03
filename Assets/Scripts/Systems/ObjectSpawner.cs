using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawnable Objects")]
    [SerializeField]
    private GameObject[] objects;

    [Header("Spawn settings")]
    [SerializeField]
    private Vector3[] spawnPoints;
    [SerializeField]
    private float timeBetweenSpawns;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    void Spawn()
    {
        Instantiate(objects[Random.Range(0, objects.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity);
    }
}
