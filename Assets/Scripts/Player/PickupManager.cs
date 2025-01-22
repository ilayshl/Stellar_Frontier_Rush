using UnityEngine;

/// <summary>
/// Responsible of the creation and behaviour of the pickups.
/// </summary>
public class PickupManager : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites; //0- Health, 1- Damage, 2- Fire rate, 3- Move speed;
    private int pickupType;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetRandomPickupType(sprites.Length);
    }

    private void Start()
    {
        Destroy(gameObject, 10);
        Invoke("ActivateCollider", 1);
    }

    //Rolls for random index from 0 to a given int.
    private void SetRandomPickupType(int range)
    {
        pickupType = Random.Range(0, range);
        sr.sprite = sprites[pickupType];
    }

    /// <summary>
    /// Sets a specific type to the pickup from the pre-set array.
    /// 0- Health, 1- Damage, 2- Fire rate, 3- Movement speed.
    /// </summary>
    /// <param name="index"></param>
    public void SetPickupType(int index)
    {
        pickupType = index;
        sr.sprite = sprites[index];
    }

    //Checks for the pickup type and collision object's type on collision with another object
    //then, in case of a player, grants it the specific pickup buff.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController pController))
        {
            switch (pickupType)
            {
                case 0:
                    pController.IncreaseHealth(1);
                    break;
                case 1:
                    pController.IncreaseDamage(1);
                    break;
                case 2:
                    pController.IncreaseFireRate(5);
                    break;
                case 3:
                    pController.IncreaseSpeed(1);
                    break;

            }
            Destroy(gameObject);
        }
    }

    //Activates the Collider component os the player can collide with it.
    private void ActivateCollider()
    {
        if (TryGetComponent<Collider2D>(out Collider2D collider))
        {
            collider.enabled = true;
        }
        else
        {
            Debug.LogError("Couldn't activate the collider of the pickup.");
        }
    }
}
