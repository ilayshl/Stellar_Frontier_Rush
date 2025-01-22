using UnityEngine;

/// <summary>
/// Responsible for the Meteor enemy in the game that goes straight for the base.
/// </summary>
public class Meteor : MonoBehaviour
{
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
    private WaveManager waveManager;
    private SpriteRenderer sr;

    void Awake()
    {
        hp = GetComponent<HitPoints>();
        waveManager = GetComponentInParent<WaveManager>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        targetObject = FindFirstObjectByType<Base>().transform;
        RollForVariant(50);
        RollForRotationDirection();
    }

    void Update()
    {
        RotateObject();
        MoveTowardsTarget(targetObject);
    }

    //Decides if the rotation should be positive (right) or negative (left).
    private void RollForRotationDirection()
    {
        if (Random.Range(0, 2) == 1)
        {
            rotationDirection *= -1;
        }
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
    /// Takes damage and checks if Health equals or lower than 0.
    /// </summary>
    /// <param name="dmg"></param>
    public void OnHit(int dmg)
    {
        hp.LoseHealth(dmg);
        if (hp.IsDead())
        {
            waveManager.PlaySound(deathSound);
            waveManager.MeteorDestroyed(this.transform, variantIndex);
            waveManager.SpawnDeathParticles(this.transform, deathParticle);
            Destroy(gameObject);
        }
        else
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            waveManager.PlaySound(hitSounds[randomIndex]);
        }
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
}
