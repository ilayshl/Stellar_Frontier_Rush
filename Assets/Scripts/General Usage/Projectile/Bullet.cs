using UnityEngine;

/// <summary>
/// Responsible for every bullet in the game- moevment and behaviour.
/// </summary>
public class Bullet : MonoBehaviour
{
    public bool canDamagePlayer { get; private set; } = false; 
    [SerializeField] protected float speed;
    [SerializeField] private Vector3 trajectory;
    protected int dmg = 2;

    protected virtual void Start()
    {
        Destroy(gameObject, 7);
    }

    protected virtual void Update()
    {
        SetTrajectory(trajectory);
    }

    //Sets the movement direction of the object.
    private void SetTrajectory(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    /// <summary>
    /// Damages whatever the object hits, then gets destroyed.
    /// Action may vary, depending on what object the bullet hit.
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDamagePlayer)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnHit(dmg);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.TryGetComponent<PlayerCollider>(out PlayerCollider pCollider))
            {
                pCollider.OnHit(dmg);
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Sets the damage that the object deals when it collides with another collider.
    /// </summary>
    /// <param name="newDamage"></param>
    public void SetDamage(int newDamage)
    {
        dmg = newDamage;
    }
}
