using UnityEngine;

public class BossCollider : MonoBehaviour
{
    private Animator _animator;
    private BossManager _bossManager;

    void Awake()
    {
        _bossManager = GetComponentInParent<BossManager>();
        _animator = GetComponent<Animator>();
        GetComponent<Collider2D>().enabled = true;
    }

    public void OnHit(int dmg)
    {
        _bossManager.hp.LoseHealth(dmg);
        if (_bossManager.hp.IsDead())
        {
            _bossManager.OnDeath();
        }
        else
        {
            _animator.SetTrigger("onHit");
            //int randomIndex = Random.Range(0, hitSounds.Length);
            //enemyManager.PlaySound(hitSounds[randomIndex]);
        }
    }
}
