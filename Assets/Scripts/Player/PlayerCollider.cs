using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    private Animator animator;
    private Base playerBase;

    private const int PLAYER_DAMAGE = 1;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerBase = FindFirstObjectByType<Base>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D))
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
        playerBase.ChangeHealth(-damage);
        SoundManager.PlaySound(hurtSound, true);
    }
}
