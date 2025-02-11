using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    private Base playerBase;
    private SoundManager soundManager;

    private const int PLAYERDAMAGE = 1;

    private void Awake()
    {
        playerBase = FindFirstObjectByType<Base>();
        soundManager = GetComponentInParent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.OnHit(PLAYERDAMAGE);
            playerBase.ChangeHealth(-PLAYERDAMAGE);
            soundManager.PlaySound(hurtSound);
        }
    }
}
