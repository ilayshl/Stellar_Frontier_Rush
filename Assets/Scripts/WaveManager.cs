using UnityEngine;

/// <summary>
/// Spawns waves with a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject[] waves;
    [SerializeField] private GameObject[] enemyToSpawn;

    private int enemiesAlive;
    private float newWaveCooldown = 5;

    void Start()
    {
        SpawnFirstWave();
    }

    private void SpawnFirstWave()
    {
        var wave = Instantiate(firstWave);
        ReplacePlaceholders(wave.transform, 0);
        Destroy(wave);
    }

    public void EnemyKilled() {
        enemiesAlive--;
        if(enemiesAlive <= 0) {
            Invoke("SpawnNextWave", newWaveCooldown);
        }
    }

    private void SpawnNextWave() {
        var wave = Instantiate(waves[GetRandomIndex(waves)]);
        ReplacePlaceholders(wave.transform, GetRandomIndex(enemyToSpawn));
        Destroy(wave);
    }

    //Replaces each placeholder in the Wave Prefab and sets the enemies as children of WaveManager instead of WavePrefab.
    private void ReplacePlaceholders(Transform source, int enemyType) {
        foreach(Transform point in source) {
            if(point.gameObject.CompareTag("Placeholder")) {
                var position = point.transform.position;
                Instantiate(enemyToSpawn[enemyType], position, Quaternion.identity, transform);
                Destroy(point.gameObject);
                enemiesAlive++;
            }
        }
    }
    private int GetRandomIndex(GameObject[] source) {
        return Random.Range(0, source.Length);
    }
}