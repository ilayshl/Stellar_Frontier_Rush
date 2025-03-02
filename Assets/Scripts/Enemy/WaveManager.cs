using UnityEngine;

/// <summary>
/// Spawns waves after a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject[] wavesSwing; //Side to side gradually descending
    [SerializeField] private GameObject[] enemySwingToSpawn; //Aliens
    [SerializeField] private GameObject[] wavesDirect; //Head on to the base
    [SerializeField] private GameObject[] enemyDirectToSpawn; //Meteors
    private int waveCounter;
    private float newWaveCooldown = 5;
    private float difficultyTimer;
    private bool isSpawningWave = false;

    private const int DIRECTWAVERATE = 4;
    private const int SCREEN = 16;

    void Start()
    {
        Invoke("SpawnFirstWave", 2.5f);
        difficultyTimer = 0;
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;
    }

    //Spawns wave number 1 with the normal type of enemies.
    private void SpawnFirstWave()
    {
        var wave = Instantiate(firstWave);
        ReplacePlaceholders(wave.transform, enemySwingToSpawn, 0);
        Destroy(wave);
    }

    /// <summary>
    /// Initiates next wave creation after a delay.
    /// </summary>
    public void StartNextWaveSequence(){
            float waveSpawnRate = DifficultyIncrement() / 3;
            Invoke("GetNextWave", Mathf.Max(0, newWaveCooldown - waveSpawnRate));
    }

    // Decides which type of wave should be spawned next
    private void GetNextWave()
    {
        if(!isSpawningWave)
        {
        isSpawningWave=true;
        if (waveCounter % DIRECTWAVERATE == 0 && waveCounter != 0)
        {
            SpawnNextWave(wavesDirect, enemyDirectToSpawn);
        }
        else
        {
            SpawnNextWave(wavesSwing, enemySwingToSpawn);
        }
        }
    }

    //Spawns the given wave with given enemies
    private void SpawnNextWave(GameObject[] waves, GameObject[] enemies)
    {
        var waveToSpawn = Instantiate(waves[GetRandomIndex(waves)]);
        ReplacePlaceholders(waveToSpawn.transform, enemies, GetRandomIndex(enemies));
        Destroy(waveToSpawn);
        isSpawningWave=false;
    }

    //private void SpawnWave(EnemyType type, )

    //Replaces each placeholder in the Wave Prefab and sets the enemies as children of WaveManager instead of WavePrefab.
    //Also, decides on the direction of the wave (either left or right).
    private void ReplacePlaceholders(Transform source, GameObject[] enemies, int enemyType)
    {
        int direction = DecideDirection();
        foreach (Transform point in source)
        {
            if (point.gameObject.CompareTag("Placeholder"))
            {
                var position = point.transform.position;
                GameObject enemyInstance;
                if (waveCounter % DIRECTWAVERATE == 0 && waveCounter != 0)
                {
                    enemyInstance = Instantiate(enemies[GetRandomIndex(enemies)], position, Quaternion.identity, transform);
                if (enemyInstance.TryGetComponent<Meteor>(out Meteor meteor))
                    {
                        meteor.AddSpeed(DifficultyIncrement()/2);
                    }
                }
                else
                {
                    Vector3 newPosition = new Vector3(position.x+SCREEN*-direction, position.y, position.z);
                    enemyInstance = Instantiate(enemies[enemyType], newPosition, Quaternion.identity, transform);
                    if (enemyInstance.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.AddSpeed(DifficultyIncrement());
                        enemy.SetDirection(direction);

                    }
                }
                Destroy(point.gameObject);
            }
        }
        waveCounter++;
    }

    //Every 10 seconds, adds 0.7 moveSpeed to each enemy.
    private float DifficultyIncrement()
    {
        float increment = 0;
        for (int i = 1; i <= difficultyTimer; i++)
        {
            if (i % 10 == 0)
            {
                increment += 0.7f;
            }
        }
        return increment;
    }

    //Gives a value of 1 (continue in the same direction) or -1 (flips the direction).
    private int DecideDirection()
    {
        int direction = 1;
        if (Random.Range(0, 100) <= 50)
        {
            direction *= -1;
        }
        return direction;
    }
    
    //Returns a random index int of a given GameObject array.
    private int GetRandomIndex(GameObject[] source)
    {
        return Random.Range(0, source.Length);
    }
}