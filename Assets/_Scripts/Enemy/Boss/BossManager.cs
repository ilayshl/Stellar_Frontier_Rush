using System.Collections;
using UnityEngine;

/// <summary>
/// Responsible for boss behavior. 
/// </summary>
public class BossManager : Enemy
{
    [SerializeField] private Transform shootTransform; //Shooting starting position
    [Header("Sounds")]
    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private AudioClip[] sweepSound;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip swingSound;

    private Transform playerTransform;
    private Shoot _shoot;
    private Vector2 startingPosition;
    private bool isOriginalBoss;
    private WaveManager _waveManager;
    private Coroutine _currentCoroutine;

    private const float EDGEX = 7.5f;

    protected override void Awake()
    {
        base.Awake();
        playerTransform = FindFirstObjectByType<PlayerController>().transform;
        _waveManager = enemyManager.GetComponent<WaveManager>();
        _shoot = GetComponent<Shoot>();
        GetComponentInChildren<Collider2D>().enabled = true;
    }

    protected override void Start()
    {
        enemyManager.AddToCurrentWave(this);
    }

    /// <summary>
    /// Returns the starting health of the boss, decided by the player's current damage stat.
    /// </summary>
    /// <returns></returns>
    private int StartingHealth()
    {
        int playerDamage = (int)PlayerManager.Instance.GetStatValue(StatType.Damage);
        return playerDamage * initialHP;
    }

    /// <summary>
    /// Gets the information whether it's the original boss or split ones, then sets health accordingly.
    /// </summary>
    /// <param name="healthMultiplier"></param>
    public void InitiateBoss(bool isFirstBoss)
    {
        this.hp = new HitPoints(StartingHealth());
        startingPosition = transform.position;
        if (isFirstBoss)
        {
            transform.position += (Vector3)new Vector2(0, 10);
            ChangeState(BossStates.Intro);
        }
        else
        {
            hp.SetHealth(hp.currentHP / 2);
            ChangeState(BossStates.Swing);
        }
        this.isOriginalBoss = isFirstBoss;
    }

    /// <summary>
    /// Changes state, decided by the argument.
    /// </summary>
    /// <param name="state"></param>
    // The argument of BossStates is then converted to a string in order to call a corresponding coroutine.
    private void ChangeState(BossStates state = BossStates.Swing)
    {
       if(_currentCoroutine != null ) { StopCoroutine(_currentCoroutine); }
        _currentCoroutine = StartCoroutine(state.ToString());
    }

    /// <summary>
    /// Chooeses a random attack from the pre-set indexes of BossStates.
    /// </summary>
    private void RandomAttack()
    {
        int randomIndex = Random.Range(3, 7);
        ChangeState((BossStates)randomIndex);
    }

    /// <summary>
    /// Takes the boss down from outside the screen to its starting point.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Intro()
    {
        SoundManager.PlaySound(introSound);
        Vector2 currentPosition = transform.position;
        while (Vector2.Distance(currentPosition, startingPosition) > 0.05f)
        {
            currentPosition = Vector2.Lerp(currentPosition, startingPosition, moveSpeed * Time.deltaTime / 2);
            transform.position = currentPosition;
            yield return null;
        }
        ChangeState(BossStates.Swing);
    }

    private IEnumerator Swing()
    {
        SoundManager.PlaySound(swingSound, true);
        int randomTime = Random.Range(3, 5);
        float timePassed = 0;
        while (timePassed < randomTime)
        {
            transform.position += new Vector3(moveDir * moveSpeed * Time.deltaTime, 0, 0);
            CheckForScreenEdges();
            timePassed += Time.deltaTime;
            yield return null;
        }
        if (isOriginalBoss)
        {
            if (hp.currentHP <= hp.initialHP / 2)
            {
                ChangeState(BossStates.Split);
                yield break;
            }
        }
        RandomAttack();
    }

    /// <summary>
    /// Returns the object to startingPoint.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Return()
    {
        Vector2 currentPosition = transform.position;
        while (Vector2.Distance(currentPosition, startingPosition) > 0.05f)
        {
            currentPosition = Vector2.Lerp(currentPosition, startingPosition, moveSpeed * Time.deltaTime);
            transform.position = currentPosition;
            yield return null;
        }
        ChangeState();
    }

    /// <summary>
    /// Shots random amount of projectile waves at the player.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(.5f);
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
        {
            SoundManager.PlaySound(fireSound, true);
            ShootProjectile();
            yield return new WaitForSeconds(0.75f);
        }
        ChangeState();
    }

    /// <summary>
    /// Quickly charges towards the player position, set when the coroutine starts.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Charge()
    {
        SoundManager.PlaySound(chargeSound);
        Vector2 targetPosition = playerTransform.position;
        Vector2 currentPosition = this.transform.position;
        while (Vector2.Distance(currentPosition, targetPosition) > 0.1f)
        {
            currentPosition = Vector2.Lerp(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = currentPosition;
            yield return null;
        }
        ChangeState(BossStates.Return);
    }

    /// <summary>
    /// Goes up, then sweeps at the player's y location for a random amount of times.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Sweep()
    {
        yield return StartCoroutine(GoUp());
        int random = Random.Range(1, 3);
        Vector2 offset = new Vector3(16, 0);
        for (int i = 0; i < random; i++)
        {
            SoundManager.PlaySound(sweepSound[i]);
            Vector2 targetPosition = (Vector2)playerTransform.position + (offset * moveDir);
            Vector2 currentPosition = (Vector2)playerTransform.position + (-offset * moveDir);
            while (Vector2.Distance(currentPosition, targetPosition) > 0.05f)
            {
                currentPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime * 7);
                transform.position = currentPosition;
                yield return null;
            }
            ChangeDirection();
        }
        ChangeState(BossStates.Return);
    }

    /// <summary>
    /// Spawns a wave of random enemies, including meteors (once I'll understand how to work with Inheritance).
    /// </summary>
    /// <returns></returns>
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(.5f);
        int random = Random.Range(1, 4);
        for (int i = 0; i < random; i++)
        {
            SoundManager.PlaySound(spawnSound, true);
            Vector3 spawnPosition = transform.position - new Vector3(0, 1.2f, 0);
            _waveManager.SpawnEnemy(null, spawnPosition, moveDir);
            yield return new WaitForSeconds(.5f);
        }
        ChangeState();
    }

    /// <summary>
    /// When reaching <50% health, splits into 2 bosses with smaller hitboxes.
    // These bosses have no intro animation and will attack independantly.
    /// </summary>
    /// <returns></returns>
    private IEnumerator Split()
    {
        GetComponentInChildren<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(SpawnSplits());
        OnDeath();
    }

    /// <summary>
    /// Goes up, outside of the camera bounds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoUp()
    {
        Vector2 targetPosition = transform.position + new Vector3(0, 4, 0);
        Vector2 currentPosition = this.transform.position;
        //Goes up from the screen
        while (Vector2.Distance(currentPosition, targetPosition) > 0.05f)
        {
            currentPosition = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime * 3);
            transform.position = currentPosition;
            yield return null;
        }
    }

    /// <summary>
    /// Spawns the split bosses in the current location.
    /// This needs to be a coroutine to make runtime actions work better.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnSplits()
    {
        Vector3 offset = new Vector3(1f, 0, 0);
        for (int i = 0; i < 2; i++)
        {
            offset *= -1;
            var splitBoss = Instantiate(this.gameObject, transform.position + offset, Quaternion.identity, _waveManager.gameObject.transform);
            splitBoss.GetComponent<BossManager>().InitiateBoss(false);
            splitBoss.transform.localScale *= 0.7f;

        }
        yield return new WaitForEndOfFrame();
    }
    /// <summary>
    /// Checks if x position is touching the screen edges.
    /// </summary>
    private void CheckForScreenEdges()
    {
        if ((transform.position.x >= EDGEX && moveDir > 0)
        || (transform.position.x <= -EDGEX && moveDir < 0))
        {
            ChangeDirection();
        }
    }

    /// <summary>
    /// Flips direction of movement.
    /// </summary>
    private void ChangeDirection()
    {
        moveDir *= -1;
    }

    /// <summary>
    /// Resets the object's rotation to its default.
    /// </summary>
    private void ResetRotation()
    {
        transform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// Shoots a projectile out of shootTransform's position.
    /// </summary>
    private void ShootProjectile()
    {
        _shoot.ShootBullet(shootTransform.position);
    }

    /// <summary>
    /// Makes the enemy take x damage.
    /// </summary>
    /// <param name="dmg"></param>
    public override void OnHit(int dmg)
    {
        animator.SetTrigger("onHit");
        base.OnHit(dmg);
    }

    /// <summary>
    /// When health reaches 0, dies.
    /// </summary>
    protected override void OnDeath()
    {
        var pickupSpawned = enemyManager.SpawnPickup(transform.position);
        Pickup pickup = pickupSpawned.GetComponent<Pickup>();
        pickup.StartCoroutine(pickup.RandomizePickup());
        base.OnDeath();
    }
}
