using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Responsible for every bullet in the game- moevment and behaviour.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 trajectory;
    private int dmg = 1;

    private void Start()
    {
        Invoke("DestroyBullet", 7f);
    }
    private void Update()
    {
        SetTrajectory(trajectory);
    }

    //Sets the movement direction of the object.
    private void SetTrajectory(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    //Damages whatever the object hits, then gets destroyed.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(dmg + 1);
            DestroyBullet();
        }
    }

    //Destroys the object.
    private void DestroyBullet()
    {
        if (!this.IsDestroyed())
        {
            Destroy(gameObject);
        }
    }

    //Sets the damage that the object deals when it collides with another collider.
    public void SetDamage(int newDamage)
    {
        dmg = newDamage;
    }

    /// <summary>
    /// Decides the new direction of the bullet in Vector3;
    /// Change Y values for different directions
    /// </summary>
    /// <param name="newDirection"></param>
    public void ChangeDirection(Vector3 newDirection)
    {
        trajectory = newDirection;
        if (newDirection == Vector3.up)
        {
            transform.rotation = Quaternion.identity;
        }
    }
}
