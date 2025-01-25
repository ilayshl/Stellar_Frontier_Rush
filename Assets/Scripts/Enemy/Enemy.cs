using UnityEngine;

/// <summary>
/// Responsible for enemies in the game and dropping pickups on death.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip deathSound;
    private float xEdge = 7.5f;
    private int moveDir = 1;
    private HitPoints hp;
    private EnemyManager enemyManager;
    private SpriteRenderer sr;
    private Animator animator;
    private const int dmg = 1;

    private void Awake()
    {
        hp = GetComponent<HitPoints>();
        enemyManager = GetComponentInParent<EnemyManager>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start() {
        enemyManager.AddToCurrentWave(gameObject);
    }

    private void Update()
    {
        Move();
        CheckForScreenEdges();
    }

    //Returns value of const int dmg.
    public int Damage()
    {
        return dmg;
    }

    //Changes transform.position by the direction and moveSpeed.
    private void Move()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
    }

    //Changes direction when touching screen edges.
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= xEdge && moveDir > 0)
        || (transform.position.x <= -xEdge && moveDir < 0))
        {
            RowDown();
        }
    }

    //Moves 1 row down (closer to the player) and changes the movement direction.
    private void RowDown()
    {
        SetDirection(-1);
        transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
    }

    /// <summary>
    /// Adds moveSpeed to the enemy.
    /// </summary>
    /// <param name="addition"></param>
    public void AddSpeed(float addition)
    {
        moveSpeed += addition;
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
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        animator.SetTrigger("onHit");
        if (hp.IsDead())
        {
            enemyManager.PlaySound(deathSound);
            enemyManager.EnemyKilled(gameObject);
            enemyManager.SpawnDeathParticles(this.transform, deathParticle);
            Destroy(gameObject);
        }
        else
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            enemyManager.PlaySound(hitSounds[randomIndex]);
        }
        if (hp.Health() == 1)
        {
            if (sr.color == Color.green) //If enemy is Berserker
            {
                float berserkerBonus = 1.5f;
                moveSpeed *= berserkerBonus;
            }
        }
    }
}
