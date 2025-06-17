using UnityEngine;

/// <summary>
/// Responsible for enemies in the game and dropping pickups on death.
/// </summary>
public class Enemy : MonoBehaviour
{
    public int myScore = 50;
    
    [Header("Basic Stats")]
    [SerializeField] protected float moveSpeed = 1;
    [SerializeField] protected int initialHP = 4;
    [SerializeField] private int dmg;
    [Header("Basic Assets")]
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject deathParticle;

    protected int moveDir = 1;
    protected EnemyManager enemyManager;
    protected SpriteRenderer sr;
    protected Animator animator;
    protected HitPoints hp;

    //Awake and Start are protected virtual so they cab be overriden.
    protected virtual void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        sr = GetComponentInChildren<SpriteRenderer>();
        if(GetComponentInChildren<Animator>() != null)
        {
        animator = GetComponentInChildren<Animator>();
        }

    }

    protected virtual void Start()
    {
        hp = new HitPoints(initialHP);
        enemyManager.AddToCurrentWave(this);
    }

    /// <summary>
    /// Returns the damage of the enemy.
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return dmg;
    }

    /// <summary>
    /// Sets the direction of the object by an int value- negative is left, positive is right.
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(int direction)
    {
        moveDir *= direction;
    }

    /// <summary>
    /// Makes the enemy take x damage, if dead, triggers death events.
    /// </summary>
    /// <param name="dmg"></param>
    public virtual void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        if (hp.IsDead())
        {
            OnDeath();
        }
        else
        {
            int randomIndex = hitSounds.Length > 1 ? Random.Range(0, hitSounds.Length) : 0;
            SoundManager.PlaySound(hitSounds[randomIndex]);
        }
    }

    /// <summary>
    /// When hp reaches 0.
    /// </summary>
    protected virtual void OnDeath()
    {
        SoundManager.PlaySound(deathSound);
        enemyManager.EnemyKilled(this);
        enemyManager.AddScore(myScore);
        enemyManager.SpawnDeathParticles(this.transform, deathParticle);
        Destroy(gameObject);
    }

    /// <summary>
    /// Adds moveSpeed to the enemy.
    /// </summary>
    /// <param name="addition"></param>
    public virtual void AddSpeed(float addition)
    {
        this.moveSpeed += addition;
    }

}
