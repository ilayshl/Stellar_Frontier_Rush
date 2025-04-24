using UnityEngine;

/// <summary>
/// Responsible of the creation and behaviour of the pickups.
/// </summary>
public class Pickup : MonoBehaviour
{
    public StatType pickupType {get; private set;}
    [SerializeField] private Sprite[] sprites; //0- Health, 1- Damage, 2- Fire rate, 3- Move speed, 4- Missile;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        SetRandomPickupType(sprites.Length - 1); //To not get Missile
    }

    private void Start()
    {
        Destroy(gameObject, 10);
        Invoke("ActivateCollider", 1);
    }

    //Rolls for random index from 0 to a given int.
    private void SetRandomPickupType(int range)
    {
        pickupType = (StatType)Random.Range(0, range);
        sr.sprite = sprites[(int)pickupType];
    }

    /// <summary>
    /// Sets a specific type to the pickup from the pre-set array.
    /// 0- Health, 1- Damage, 2- Fire rate, 3- Movement speed.
    /// </summary>
    /// <param name="index"></param>
    public void SetPickupType(StatType type)
    {
        pickupType = type;
        sr.sprite = sprites[(int)type];
    }

    //Checks for the pickup type and collision object's type on collision with another object
    //then, in case of a player, grants it the specific pickup buff.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerCollider>(out PlayerCollider pCollider))
        {
            PlayerStats pStats = pCollider.GetComponentInParent<PlayerStats>();
            pStats.ChangeStat(pickupType, 1);
            Destroy(gameObject);
        }
    }

    //Activates the Collider component so the player can collide with it.
    private void ActivateCollider()
    {
        GetComponentInChildren<Collider2D>().enabled = true;
    }
}
