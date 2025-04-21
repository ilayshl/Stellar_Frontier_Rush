using UnityEngine;

public class BaseCollider : MonoBehaviour
{
    private Base baseManager;
    private Collider2D baseCollider;
    private bool isDead = false;

    void Awake()
    {
        baseManager = this.GetComponentInParent<Base>();
        baseCollider = this.GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!isDead)
        {
        if (other.transform.parent.TryGetComponent<Enemy>(out Enemy enemy) && other.transform.parent.GetComponent<BossManager>() == null)
        {
            baseManager.EnemyHit(enemy);
        }
        }
    }
    
    public void SpawnDeathParticles()
    {
        baseManager.SpawnDeathParticles(baseCollider);
        //baseCollider.enabled = false;
        if(!isDead){ isDead = true; }
    }
}
