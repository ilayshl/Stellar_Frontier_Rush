using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Responsible for every bullet in the game- moevment and behaviour.
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 trajectory;
    [SerializeField] private int dmg;
    private void Start()
    {
        Invoke("DestroyBullet", 7f);
    }
    private void Update()
    {
        SetTrajectory(trajectory);
    }

    private void SetTrajectory(Vector3 direction)
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(dmg);
            DestroyBullet();
        }
    }

    private void DestroyBullet(){
        if(!this.IsDestroyed()){
            Destroy(gameObject);
        }
    }
}
