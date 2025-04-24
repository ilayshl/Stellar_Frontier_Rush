using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    private Base playerBase;

    private const int PLAYER_DAMAGE = 1;

    private void Awake()
    {
        playerBase = FindFirstObjectByType<Base>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            OnHit(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(PLAYER_DAMAGE);
            OnHit(enemy.Damage());
        }
    }

    public void OnHit(int damage)
    {
        playerBase.ChangeHealth(-damage);
        SoundManager.PlaySound(hurtSound, true);
    }
}
