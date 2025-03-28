using UnityEngine;

/// <summary>
/// Responsible for the Meteor enemy in the game that goes straight for the base.
/// The Meteor enemy goes straight for the base and hits it harder than a Swing enemy.
/// </summary>
public class Meteor : Enemy
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Sprite[] variants;

    private int rotationDirection = 1; //1 = right, -1 = left.
    private int variantIndex; //0= Normal, 1= Damage, 2= Fire rate, 3= Move speed

    private Transform targetObject;

    protected override void Start()
    {
        base.Start();
        targetObject = FindFirstObjectByType<Base>().transform;
        RollForVariant(50);
        RollForRotationDirection();
    }

    private void Update()
    {
        RotateObject();
        MoveTowardsTarget(targetObject);
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

    protected override void OnDeath()
    {
        if (variantIndex > 0)
        {
            var pickupSpawned = enemyManager.SpawnPickup(transform);
            PickupManager pickupManager = pickupSpawned.GetComponent<PickupManager>();
            pickupManager.SetPickupType(variantIndex);
        }
        base.OnDeath();
    }
}
