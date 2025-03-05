using UnityEngine;

public class BossCollider : MonoBehaviour
{
    private Animator _animator;
    private BossManager _bossManager;
    // Start is called before the first frame update
    void Awake()
    {
        _bossManager = GetComponentInParent<BossManager>();
        _animator = GetComponent<Animator>();
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
