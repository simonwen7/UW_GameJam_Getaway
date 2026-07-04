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
    [SerializeField]
    private int[] weights;

    private float currentTime;

    private int totalWeight;

    void Awake()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            totalWeight += weights[i];
        }
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isLevelCompleted) return;

        currentTime += Time.deltaTime;

        if (currentTime >= timeBetweenSpawns)
        {
            Spawn();
            currentTime -= timeBetweenSpawns;
        }
    }

    void Spawn()
    {
        int n = Random.Range(0, totalWeight);

        int cumulative = 0;

        for (int i = 0; i < objects.Length; i++)
        {
            cumulative += weights[i];

            if (n < cumulative)
            {
                Instantiate(objects[i], spawnPoints[Random.Range(0, spawnPoints.Length)], Quaternion.identity);
                return;
            }
        }
    }
}
