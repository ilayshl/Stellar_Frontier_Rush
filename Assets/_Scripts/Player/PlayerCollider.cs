using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    private Animator animator;

    private const int PLAYER_DAMAGE = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnHit(1);
        }
    }

    /// <summary>
    /// Gets the trigger enter from an enemy.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(PLAYER_DAMAGE);
            animator.SetTrigger("isHurt");
            OnHit(enemy.Damage());
        }
    }

    /// <summary>
    /// On Hit effect for the base.
    /// </summary>
    /// <param name="damage"></param>
    public void OnHit(int damage)
    {
        PlayerManager.Instance.ChangeStat(StatType.Health, -damage);
        SoundManager.PlaySound(hurtSound, true);
    }

    public void OnPickupObtained(StatType stat)
    {
        switch (stat)
            {
                case StatType.Health:
                PlayerManager.Instance.ChangeStat(StatType.Health, 1);
                break;
                case StatType.Damage:
                PlayerManager.Instance.ChangeStat(StatType.Damage, 1);
                break;
                case StatType.FireRate:
                PlayerManager.Instance.ChangeStat(StatType.FireRate, 5);
                break;
                case StatType.MoveSpeed:
                PlayerManager.Instance.ChangeStat(StatType.MoveSpeed, 1);
                break;
                case StatType.Missile:
                PlayerManager.Instance.ChangeStat(StatType.Missile, 1);
                break;
            }
    }
}
