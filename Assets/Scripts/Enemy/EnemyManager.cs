using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds current wave data and enemy events.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject pickup;
    private List<EnemyType> currentWave = new List<EnemyType>();
    private WaveManager waveManager;
    private SoundManager soundManager;
    private ScoreManager scoreManager;

    private void Awake()
    {
        waveManager = GetComponent<WaveManager>();
        soundManager = GetComponent<SoundManager>();
        scoreManager = GetComponent<ScoreManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) //Meant for play-testing
        {
            SpawnPickup(this.transform);
        }
    }

    //Removes the given object from the current wave list, for when the enemy dies.
    private void RemoveFromCurrentWave(EnemyType type)
    {
        if (currentWave.Contains(type))
        {
            currentWave.Remove(type);
        }
        if (currentWave.Count <= 0)
        {
            waveManager.StartNextWaveSequence();
        }
    }

    /// <summary>
    /// Add a GameObject to the current wave list.
    /// </summary>
    /// <param name="enemy"></param>
    public void AddToCurrentWave(EnemyType type)
    {
        if (!currentWave.Contains(type))
        {
            currentWave.Add(type);
        }
    }

    /// <summary>
    /// Basic enemy killed- rolls for Pickup spawn after death.
    /// </summary>
    /// <param name="enemyTransform"></param>
    public void EnemyKilled(EnemyType type)
    { 
        RemoveFromCurrentWave(type);
    }

    //Spawns a pickup in the given position and returns itself as a variable.
    private GameObject SpawnPickup(Transform transform)
    {
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

    /// <summary>
    /// Adds int to the Score UI.
    /// </summary>
    /// <param name="addition"></param>
    public void AddScore(int addition){
        scoreManager.AddScore(addition);
    }

    /// <summary>
    /// Plays an AudioClip via SoundManager.
    /// </summary>
    /// <param name="sound"></param>
    public void PlaySound(AudioClip sound)
    {
        soundManager.PlaySound(sound);
    }
}
