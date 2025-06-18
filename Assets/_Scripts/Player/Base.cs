using UnityEngine;

/// <summary>
/// Responsible for the player's base. With each enemy striking the base, hp will be lost, until reaching 0.
/// </summary>
public class Base : MonoBehaviour
{
    [SerializeField] private AudioClip[] impactSounds;
    [SerializeField] private GameObject deathExplosion;

    private SpriteRenderer sr;
    private Animator animator;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        GameManager.Instance.OnGameStateChanged += OnDeath;
        PlayerManager.Instance.OnHealthChanged += OnHit;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStateChanged -= OnDeath;
        PlayerManager.Instance.OnHealthChanged -= OnHit;

    }

    /// <summary>
    /// Enemy enters the collision of the base.
    /// </summary>
    /// <param name="enemy"></param>
    public void EnemyHit(Enemy enemy)
    {
        PlayerManager.Instance.ChangeStat(StatType.Health, -enemy.Damage());
        enemy.OnHit(100);
    }

    //For when the base takes damage.
    private void OnHit(StatType stat, int damage)
    {
        if (stat == StatType.Health)
        {
            int randomIndex = Random.Range(0, impactSounds.Length);
            SoundManager.PlaySound(impactSounds[randomIndex], true);
            animator.SetTrigger("isHurt");
        }
    }

    //Checks if HP equals or is lower than 0.
    private void OnDeath(GameState state)
    {
        if (state == GameState.Dead)
        {
            animator.SetTrigger("isDead");
        }
    }

    /// <summary>
    /// Spawns death particles at random position inside the object's collider.
    /// </summary>
    public void SpawnDeathParticles(Collider2D baseCollider)
    {
        float randomX = Random.Range(baseCollider.bounds.min.x, baseCollider.bounds.max.x);
        float randomY = Random.Range(baseCollider.bounds.min.y, baseCollider.bounds.max.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);
        var particles = Instantiate(deathExplosion, spawnPosition, Quaternion.identity);
        float randomScale = Random.Range(0.6f, 0.7f);
        particles.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
        Destroy(particles, 2);
        int randomIndex = Random.Range(0, impactSounds.Length);
        SoundManager.PlaySound(impactSounds[randomIndex], true);
    }
}
