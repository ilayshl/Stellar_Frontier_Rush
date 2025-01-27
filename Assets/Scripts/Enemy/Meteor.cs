using UnityEngine;

/// <summary>
/// Responsible for the Meteor enemy in the game that goes straight for the base.
/// </summary>
public class Meteor : MonoBehaviour
{
    public int myScore = 50;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int dmg;
    [SerializeField] private Sprite[] variants;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip deathSound;
    private int rotationDirection = 1; //1 = right, -1 = left.
    private int variantIndex; //0= Normal, 1= Damage, 2= Fire rate, 3= Move speed

    private Transform targetObject;
    private HitPoints hp;
    private EnemyManager enemyManager;
    private SpriteRenderer sr;

    void Awake()
    {
        hp = GetComponent<HitPoints>();
        enemyManager = GetComponentInParent<EnemyManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        targetObject = FindFirstObjectByType<Base>().transform;
        RollForVariant(50);
        RollForRotationDirection();
        enemyManager.AddToCurrentWave(gameObject);
    }

    void Update()
    {
        RotateObject();
        MoveTowardsTarget(targetObject);
    }

    /// <summary>
    /// Returns the damage value of the enemy.
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

    /// <summary>
    /// Takes damage and checks if Health equals or lower than 0.
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

    private void OnDeath()
    {
        enemyManager.PlaySound(deathSound);
        enemyManager.MeteorDestroyed(gameObject, variantIndex);
        enemyManager.AddScore(myScore);
        enemyManager.SpawnDeathParticles(this.transform, deathParticle);
        Destroy(gameObject);
    }
}
