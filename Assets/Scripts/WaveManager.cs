using UnityEngine;

/// <summary>
/// Spawns waves with a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyToSpawn;

    void Start()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        foreach (Transform point in transform)
        {
            if (point.gameObject.CompareTag("Placeholder"))
            {
                var position = point.transform.position;
                Instantiate(enemyToSpawn, position, Quaternion.identity, transform);
                Destroy(point.gameObject);
            }
        }
    }
}