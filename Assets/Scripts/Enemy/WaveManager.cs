using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private GameObject[] bossEnemy; //The boss
    [SerializeField] private GameObject[] bossWave; //Starting position for boss
    private int waveCounter = 1;
    private float newWaveCooldown = 5;
    private float difficultyTimer;
    private bool isSpawningWave = false;

    private const int DIRECT_WAVE_RATE = 4;
    private const int BOSS_WAVE_RATE = 10;
    private const int SCREEN = 16;

    void Start()
    {
        Invoke("SpawnFirstWave", 2.5f);
        difficultyTimer = 0;
    }

    private void Update()
    {
        difficultyTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.A))
        {
            foreach(Transform enemyObject in transform)
            {
                if(enemyObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.OnHit(100);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            waveCounter++;
            Debug.Log("New wave: "+waveCounter);
        }
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
    public void StartNextWaveSequence()
    {
        float waveSpawnRate = DifficultyIncrement() / 3;
        Invoke("GetNextWave", Mathf.Max(0, newWaveCooldown - waveSpawnRate));
    }

    // Decides which type of wave should be spawned next
    private void GetNextWave()
    {
        if (!isSpawningWave)
        {
            isSpawningWave = true;
            if (waveCounter % BOSS_WAVE_RATE == 0)
            {
                SpawnNextWave(bossWave, bossEnemy);
            }
            else if (waveCounter % DIRECT_WAVE_RATE == 0)
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
        isSpawningWave = false;
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
                if (waveCounter % BOSS_WAVE_RATE == 0)
                {
                    SpawnEnemy(enemies[enemyType], position, direction);
                }
                else if (waveCounter % DIRECT_WAVE_RATE == 0 && waveCounter != 0)
                {
                    SpawnEnemy(enemies[GetRandomIndex(enemies)], position, direction);
                }
                else
                {
                    position = new Vector3(position.x + SCREEN * -direction, position.y, position.z);
                    SpawnEnemy(enemies[enemyType], position, direction);
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
                increment += 0.5f;
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

    /// <summary>
    /// Spawns an enemy.
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public GameObject SpawnEnemy(GameObject enemy, Vector3 position, int direction)
    {
        //Boss spawns null because it has no pointers for enemies
        if (enemy == null)
        {
            enemy = enemySwingToSpawn[GetRandomIndex(enemySwingToSpawn)];
        }
        
        GameObject enemyInstance = Instantiate(enemy, position, Quaternion.identity, transform);
        if (enemyInstance.TryGetComponent<SwingEnemy>(out SwingEnemy swingEnemy))
        {
            swingEnemy.AddSpeed(DifficultyIncrement());
            swingEnemy.SetDirection(direction);
        }
        else if (enemyInstance.TryGetComponent<Meteor>(out Meteor meteor))
        {
            meteor.AddSpeed(DifficultyIncrement() / 2);
        }
        else if (enemyInstance.TryGetComponent<BossManager>(out BossManager bossManager))
        {
            bossManager.InitiateBoss(true);
        }
        return enemyInstance;
    }
}