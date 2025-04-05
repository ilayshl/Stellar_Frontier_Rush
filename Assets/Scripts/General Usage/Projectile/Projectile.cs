using UnityEngine;

/// <summary>
/// Parent class of any projectile. Holds speed, damage and canDamagePlayer.
/// </summary>
public abstract class Projectile : MonoBehaviour
{

    public bool canDamagePlayer = false;
    [Header("Movement Settings")]
    [SerializeField] protected float moveSpeed;
    protected int dmg = 2;

    protected virtual void Start()
    {
        Destroy(this.gameObject, 7);
    }

    protected abstract void Update();

    protected abstract void OnHit(Vector2 position);

    /// <summary>
    /// Damages whatever the object hits, then gets destroyed.
    /// Action may vary, depending on what object the bullet hit.
    /// </summary>
    /// <param name="other"></param>
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!canDamagePlayer)
        {
            if (other.transform.parent.TryGetComponent<Enemy>(out Enemy enemy))
            {
                OnHit(transform.position);
                enemy.OnHit(dmg);
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.TryGetComponent<PlayerCollider>(out PlayerCollider pCollider))
            {
                OnHit(transform.position);
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
