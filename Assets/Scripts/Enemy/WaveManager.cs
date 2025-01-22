using UnityEngine;

/// <summary>
/// Spawns waves after a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject[] wavesSwing;
    [SerializeField] private GameObject[] enemySwingToSpawn;
    [SerializeField] private GameObject[] wavesDirect;
    [SerializeField] private GameObject[] enemyDirectToSpawn;
    [SerializeField] private GameObject pickup;

    private int enemiesAlive;
    private int waveCounter;
    private float newWaveCooldown = 5;
    private float difficultyTimer;
    private bool isSpawningWave = false;

    private const int directWaveThreshold = 4;

    private SoundManager soundManager;

    private void Awake() {
        soundManager = GetComponent<SoundManager>();
    }

    void Start()
    {
        Invoke("SpawnFirstWave", 2.5f);
        difficultyTimer = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) //Meant for play-testing
        {
            SpawnPickup(this.transform);
        }
        difficultyTimer += Time.deltaTime;
    }

    //Spawns wave number 1 with the normal type of enemies.
    private void SpawnFirstWave()
    {
        var wave = Instantiate(firstWave);
        ReplacePlaceholders(wave.transform, enemySwingToSpawn, 0);
        Destroy(wave);
    }

    // Checks for each enemy's death; if it's the last of the wave, spawns new wave after a delay.
    // Lower newWaveCooldown equals less time between waves.
    private void OnEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            float waveSpawnRate = DifficultyIncrement() / 3;
            Invoke("GetNextWave", Mathf.Max(0, newWaveCooldown - waveSpawnRate));
        }
    }

    /// <summary>
    /// Basic enemy killed- rolls for Pickup spawn after death.
    /// </summary>
    /// <param name="enemyTransform"></param>
    public void EnemyKilled(Transform enemyTransform){
        if (RollForPercentage(20))
            {
                SpawnPickup(enemyTransform);
            }
            OnEnemyDeath();
    }

    /// <summary>
    /// Meteor enemy destroyed- if it was a variant, spawns a corresponding pickup.
    /// </summary>
    /// <param name="meteorTransform"></param>
    /// <param name="meteorType"></param>
    public void MeteorDestroyed(Transform meteorTransform, int meteorType){
        if(meteorType>0)
        {
            var pickupSpawned = SpawnPickup(meteorTransform);
            PickupManager pickupManager = pickupSpawned.GetComponent<PickupManager>();
            pickupManager.SetPickupType(meteorType);
        }
        OnEnemyDeath();
    }
    
    //Spawns a pickup in the given position and returns itself as a variable.
    private GameObject SpawnPickup(Transform transform){
        return Instantiate(pickup, transform.position, Quaternion.identity);
    }

    /// <summary>
    /// Spawns GameObject at given transform.position and destroys it after.
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="particleSystem"></param>
    public void SpawnDeathParticles(Transform enemy, GameObject particleSystem)
    {
        var particles = Instantiate(particleSystem, enemy.position, Quaternion.identity);
        Destroy(particles.gameObject, 2f);
    }

    //Decides which type of wave should be spawned next
    private void GetNextWave()
    {
        if(!isSpawningWave)
        {
        isSpawningWave=true;
        if (waveCounter % directWaveThreshold == 0 && waveCounter != 0)
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

    //Replaces each placeholder in the Wave Prefab and sets the enemies as children of WaveManager instead of WavePrefab.
    //Also, decides on the direction of the wave (either left or right).
    private void ReplacePlaceholders(Transform source, GameObject[] enemies, int enemyType)
    {
        int direction = RollForDirection();
        foreach (Transform point in source)
        {
            if (point.gameObject.CompareTag("Placeholder"))
            {
                var position = point.transform.position;
                GameObject enemyInstance;
                if (waveCounter % directWaveThreshold == 0 && waveCounter != 0)
                {
                    enemyInstance = Instantiate(enemies[GetRandomIndex(enemies)], position, Quaternion.identity, transform);
                if (enemyInstance.TryGetComponent<Meteor>(out Meteor meteor))
                    {
                        meteor.AddSpeed(DifficultyIncrement()/2);
                    }
                }
                else
                {
                    enemyInstance = Instantiate(enemies[enemyType], position, Quaternion.identity, transform);
                    if (enemyInstance.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        enemy.AddSpeed(DifficultyIncrement());
                        enemy.SetDirection(direction);
                    }
                }
                Destroy(point.gameObject);
                enemiesAlive++;

            }
        }
        waveCounter++;
    }

    //Every 10 seconds, add 0.7 moveSpeed to each enemy.
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

    //Checks for a percentage out of 100.
    private bool RollForPercentage(int percentage)
    {
        int i = Random.Range(0, 100);
        if (i < percentage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Gives a value of 1 (continue in the same direction) or -1 (flips the direction).
    private int RollForDirection()
    {
        int direction = 1;
        if (RollForPercentage(50))
        {
            direction *= -1;
        }
        return direction;
    }

    /// <summary>
    /// Plays an AudioClip via SoundManager.
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(AudioClip sound){
        soundManager.PlaySound(sound);
    }
    
    //Returns a random index int of a given GameObject array.
    private int GetRandomIndex(GameObject[] source)
    {
        return Random.Range(0, source.Length);
    }
}