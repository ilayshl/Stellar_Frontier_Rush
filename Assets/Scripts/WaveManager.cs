using UnityEngine;

/// <summary>
/// Spawns waves with a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager: MonoBehaviour {
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject[] waves;
    [SerializeField] private GameObject[] enemyToSpawn;

    private int enemiesAlive;
    private float newWaveCooldown = 5;

    void Start() {
        SpawnFirstWave();
    }

    private void SpawnFirstWave() {
        var wave = Instantiate(firstWave);
        ReplacePlaceholders(wave.transform, 0);
        Destroy(wave);
    }

    //Checks for each enemy's death; if it's the last of the wave, spawns new wave after a delay.
    //Delay is calculated by the the higher between 0, or the inital wave cooldown - difficulty increment.
    //Lower newWaveCooldown equals more difficult.
    public void EnemyKilled() {
        enemiesAlive--;
        if(enemiesAlive<=0) {
            Invoke("SpawnNextWave", Mathf.Max(0, newWaveCooldown-(DifficultyIncrement()/3)));
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
                var enemyInstance = Instantiate(enemyToSpawn[enemyType], position, Quaternion.identity, transform);
                Destroy(point.gameObject);
                enemiesAlive++;
                if(enemyInstance.TryGetComponent<Enemy>(out Enemy enemy)) {
                    enemy.SetSpeed(DifficultyIncrement());
                }
            }
        }
    }

    //For every 10 seconds, add 0.5 moveSpeed to each enemy, thus making them more difficult.
    private float DifficultyIncrement() {
        int currentTime = (int)Time.time;
        float increment = 0;
        for(int i = 1; i<=currentTime; i++) {
            if(i%10==0) {
                increment+=0.5f;
            }
        }
        return increment;
    }

    private int GetRandomIndex(GameObject[] source) {
        return Random.Range(0, source.Length);
    }
}