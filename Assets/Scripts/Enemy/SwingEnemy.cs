using UnityEngine;

public class SwingEnemy : MonoBehaviour
{
    public int myScore = 50;

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private int initialHP = 4;
    [SerializeField] private int dmg;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject deathParticle;

    private int moveDir = 1;
    private EnemyManager enemyManager;
    private SpriteRenderer sr;
    private HitPoints hp;
    private Animator animator;

    private const float EDGEX = 7.5f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        hp = new HitPoints(initialHP);
        enemyManager.AddToCurrentWave(gameObject);
    }
 
    void Update()
    {
        Move();
        CheckForScreenEdges();
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

    //Changes transform.position by the direction and moveSpeed.
    private void Move()
    {
        transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
    }
    
    //Changes direction when touching screen edges.
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= EDGEX && moveDir > 0)
        || (transform.position.x <= -EDGEX && moveDir < 0))
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
    /// Makes the enemy take x damage, if dead, triggers death events.
    /// </summary>
    /// <param name="dmg"></param>
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        animator.SetTrigger("onHit");
        if (hp.IsDead())
        {
            OnDeath();
        }
        else
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            enemyManager.PlaySound(hitSounds[randomIndex]);
        }
        /*if (hp.Health() == 1)
        {
            if (sr.color == Color.green) //If enemy is Berserker
            {
                float berserkerBonus = 1.5f;
                moveSpeed *= berserkerBonus;
            }
        }*/
    }

    private void OnDeath()
    {
        enemyManager.PlaySound(deathSound);
        enemyManager.EnemyKilled(gameObject);
        enemyManager.AddScore(myScore);
        enemyManager.SpawnDeathParticles(this.transform, deathParticle);
        if(Random.Range(0, 100)<=25)
        {
            enemyManager.SpawnPickup(transform);
        }
        Destroy(gameObject);
    }

    /// <summary>
    /// Adds moveSpeed to the enemy.
    /// </summary>
    /// <param name="addition"></param>
    public void AddSpeed(float addition)
    {
        this.moveSpeed += addition;
    }

}
