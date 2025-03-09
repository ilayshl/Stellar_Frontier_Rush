using UnityEngine;

/// <summary>
/// Responsible for the Meteor enemy in the game that goes straight for the base.
/// </summary>
public class Meteor : Enemy
{
    public int myScore = 50;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Sprite[] variants;
    [SerializeField] protected float moveSpeed = 1;
    [SerializeField] protected int initialHP = 4;
    [SerializeField] private int dmg;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private GameObject deathParticle;

    protected int moveDir = 1;
    protected EnemyManager enemyManager;
    protected SpriteRenderer sr;
    private HitPoints hp;

    private int rotationDirection = 1; //1 = right, -1 = left.
    private int variantIndex; //0= Normal, 1= Damage, 2= Fire rate, 3= Move speed

    private Transform targetObject;

    void Awake()
    {
        enemyManager = GetComponentInParent<EnemyManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        hp = new HitPoints(initialHP);
        targetObject = FindFirstObjectByType<Base>().transform;
        RollForVariant(50);
        RollForRotationDirection();
        enemyManager.AddToCurrentWave(gameObject);
    }

    private void Update()
    {
        RotateObject();
        MoveTowardsTarget(targetObject);
    }

    /// <summary>
    /// Returns the damage of the enemy.
    /// </summary>
    /// <returns></returns>
    public int Damage()
    {
        return dmg;
    }

    //Rotates the object.
    private void RotateObject()
    {
        Vector3 rotation = new Vector3(0, 0, rotationSpeed * rotationDirection * Time.deltaTime);
        transform.Rotate(rotation);
    }

    //Moves the objects towards the given target.
    private void MoveTowardsTarget(Transform target)
    {
        Vector2 targetPosition = target.transform.position;
        Vector2 currentPosition = transform.position;
        transform.position =
        Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    //Decides if the rotation should be positive (right) or negative (left).
    private void RollForRotationDirection()
    {
        if (Random.Range(0, 2) == 1)
        {
            rotationDirection *= -1;
        }
    }

    //Rolls percentage, if hits sets the object as a Variant.
    private void RollForVariant(int percentage)
    {
        if (Random.Range(0, 100) <= percentage)
        {
            variantIndex = Random.Range(1, variants.Length);
        }
        else
        {
            variantIndex = 0;
        }
        sr.sprite = variants[variantIndex];
    }

    /// <summary>
    /// Adds moveSpeed and rotationSpeed to the enemy.
    /// </summary>
    /// <param name="addition"></param>
    public void AddSpeed(float addition)
    {
        moveSpeed += addition / 3;
        rotationSpeed += addition;
    }

    /// <summary>
    /// Makes the enemy take x damage, if dead, triggers death events.
    /// </summary>
    /// <param name="dmg"></param>
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        if (hp.IsDead())
        {
            OnDeath();
        }
        else
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            enemyManager.PlaySound(hitSounds[randomIndex]);
        }
    }

    private void OnDeath()
    {
        if (variantIndex > 0)
        {
            Debug.Log(variantIndex);
            var pickupSpawned = enemyManager.SpawnPickup(transform);
            PickupManager pickupManager = pickupSpawned.GetComponent<PickupManager>();
            pickupManager.SetPickupType(variantIndex);
        }
        enemyManager.PlaySound(deathSound);
        enemyManager.EnemyKilled(gameObject);
        enemyManager.AddScore(myScore);
        enemyManager.SpawnDeathParticles(this.transform, deathParticle);
        Destroy(gameObject);
    }
}
