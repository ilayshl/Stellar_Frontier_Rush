using UnityEngine;

/// <summary>
/// Responsible for the player's base. With each enemy striking the base, hp will be lost, until reaching 0.
/// </summary>
public class Base : MonoBehaviour
{
    [SerializeField] private int initialHP = 10;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioClip[] impactSounds;
    [SerializeField] private GameObject deathExplosion;

    private HitPoints hp;
    private SpriteRenderer sr;
    private Animator animator;

    private void Awake()
    {
        hp = new HitPoints(initialHP);
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    public void EnemyHit(Enemy enemy)
    {
        ChangeHealth(-enemy.Damage());
        enemy.OnHit(100);
        OnHit();
    }

    /// <summary>
    /// Changes and updates the value of HP by the amount given- either negative (decreases hp) or positive (increases hp).
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        if (amount > 0)
        {
            hp.GainHealth(amount);
        }
        else if (amount < 0)
        {
            hp.LoseHealth(amount);
        }
        else
        {
            Debug.LogError("Healing for a value of 0. Check your code.");
        }
        UpdateHealthText(hp.currentHP);
        CheckIfDead();
    }

    private void UpdateHealthText(int value)
    {
        uiManager.SetText((int)StatType.Health, value.ToString());
    }

    //For when the base takes damage.
    private void OnHit()
    {
        int randomIndex = Random.Range(0, impactSounds.Length);
        SoundManager.PlaySound(impactSounds[randomIndex], true);
        animator.SetTrigger("isHurt");
    }

    //Checks if HP equals or is lower than 0.
    private void CheckIfDead()
    {
        if (hp.IsDead())
        {
            animator.SetTrigger("isDead");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
