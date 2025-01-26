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
        soundManager = GetComponent<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.OnHit(PLAYERDAMAGE);
            }
            else if (other.TryGetComponent<Meteor>(out Meteor meteor))
            {
                meteor.OnHit(PLAYERDAMAGE);
            }
            playerBase.ChangeHealth(-PLAYERDAMAGE);
            soundManager.PlaySound(hurtSound);
        }
    }
}
