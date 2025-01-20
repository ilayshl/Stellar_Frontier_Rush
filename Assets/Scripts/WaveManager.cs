using UnityEditor.U2D.Animation;
using UnityEngine;

/// <summary>
/// Spawns waves after a delay every time the last enemy from last wave is killed.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject firstWave;
    [SerializeField] private GameObject[] waves;
    [SerializeField] private GameObject[] enemyToSpawn;
    [SerializeField] private GameObject pickup;
    [SerializeField] private GameObject deathParticle;

    private int enemiesAlive;
    private float newWaveCooldown = 5;

    void Start()
    {
        Invoke("SpawnFirstWave", 2.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SpawnPickup(this.transform);
        }
    }

    //Spawns wave number 1 with the normal type of enemies.
    private void SpawnFirstWave()
    {
        var wave = Instantiate(firstWave);
        ReplacePlaceholders(wave.transform, 0);
        Destroy(wave);
    }

    //Checks for each enemy's death; if it's the last of the wave, spawns new wave after a delay.
    //Delay is calculated by the the higher between 0, or the inital wave cooldown - difficulty increment.
    //Lower newWaveCooldown equals more difficult.
    public void EnemyKilled(Transform enemy)
    {
        SpawnDeathParticle(enemy);
        enemiesAlive--;
        if (RollForPercentage(20))
        {
            SpawnPickup(enemy);
        }
        if (enemiesAlive <= 0)
        {
            Invoke("SpawnNextWave", Mathf.Max(0, newWaveCooldown - (DifficultyIncrement() / 3)));
        }
    }

    //Spawns GameObject of Death Particles at given transform.position.
    private void SpawnDeathParticle(Transform enemy)
    {
        var particles = Instantiate(deathParticle, enemy.position, Quaternion.identity);
        Destroy(particles.gameObject, 1f);
    }

    //Spawns the next wave of enemies.
    private void SpawnNextWave()
    {
        var wave = Instantiate(waves[GetRandomIndex(waves)]);
        ReplacePlaceholders(wave.transform, GetRandomIndex(enemyToSpawn));
        Destroy(wave);
    }

    //Replaces each placeholder in the Wave Prefab and sets the enemies as children of WaveManager instead of WavePrefab.
    //Also, decides on the direction of the wave (either left or right).
    private void ReplacePlaceholders(Transform source, int enemyType)
    {
        int direction = RollForDirection();
        foreach (Transform point in source)
        {
            if (point.gameObject.CompareTag("Placeholder"))
            {
                var position = point.transform.position;
                var enemyInstance = Instantiate(enemyToSpawn[enemyType], position, Quaternion.identity, transform);
                Destroy(point.gameObject);
                enemiesAlive++;
                if (enemyInstance.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.SetSpeed(DifficultyIncrement());
                    enemy.SetDirection(direction);
                }
            }
        }
    }

    //For every 10 seconds, add 0.5 moveSpeed to each enemy, thus making them more difficult.
    private float DifficultyIncrement()
    {
        int currentTime = (int)Time.time;
        float increment = 0;
        for (int i = 1; i <= currentTime; i++)
        {
            if (i % 10 == 0)
            {
                increment += 0.5f;
            }
        }
        return increment;
    }

    //Gets a random index int of a given GameObject array.
    private int GetRandomIndex(GameObject[] source)
    {
        return Random.Range(0, source.Length);
    }

    //Checks for a percentage out of 100.
    private bool RollForPercentage(int percentage)
    {
        int i = Random.Range(0, 100);
        if (i <= percentage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Spawns a pickup at given transform.position.
    private void SpawnPickup(Transform transform)
    {
        Instantiate(pickup, transform.position, Quaternion.identity);
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
}