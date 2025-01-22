using UnityEngine;

/// <summary>
/// Responsible for every bullet in the game- moevment and behaviour.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private Vector3 trajectory;
    private int dmg;

    private void Start()
    {
        Destroy(gameObject, 7);
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
            enemy.OnHit(dmg);
            Destroy(gameObject);
        }
        else if (other.TryGetComponent<Meteor>(out Meteor meteor))
        {
            meteor.OnHit(dmg);
            Destroy(gameObject);
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
